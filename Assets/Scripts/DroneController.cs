using System;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField] private float _upForce;
    [SerializeField] private float _sideForce;
    [SerializeField] private float _landingSpeedThreshold;
    [SerializeField] private float _landingAngleThreshold;

    private Rigidbody _droneRigidbody;
    private DroneFuel _droneFuel;
    private DroneDestructor _droneDestructor;
    private DroneState _droneState;
    private Vector2 _movementDirection;

    private enum DroneState
    {
        WatingToStart,
        Normal,
        GameOver
    }
    public enum LandingState
    {
        Crashed,
        Landed
    }
        
    public static event Action<LandingState> onLandingStateChange;
    public static event Action<OnPointsPickupArgs> onPointsPickup;
    public static event Action<OnFuelPickupArgs> onFuelPickup;

    public class OnPointsPickupArgs : EventArgs
    {
        public int pointsAmount;
        public Transform pickupTransform;
    };
    public class OnFuelPickupArgs : EventArgs
    {
        public float fuelAmount;
        public Transform pickupTransform;
    };

    private void Awake()
    {
        _droneRigidbody = GetComponent<Rigidbody>();
        _droneRigidbody.sleepThreshold = 0f;
        _droneRigidbody.useGravity = false;

        _droneFuel = GetComponent<DroneFuel>();
        _droneState = DroneState.WatingToStart;

        _droneDestructor = GetComponent<DroneDestructor>();
        _droneFuel.ResetFuel();
        Debug.Log($"Im in {GetType().FullName} in Awake. Current fuel is {_droneFuel.GetCurrentFuel()}");
    }

    private void FixedUpdate()
    {
        _movementDirection = PlayerInputListener.GetInstance().GetMovementVector2();
        switch (_droneState)
        {
            case DroneState.WatingToStart:
                if (_movementDirection != Vector2.zero)
                {
                    _droneRigidbody.useGravity = true;
                    _droneState = DroneState.Normal;
                }
                break;
            case DroneState.Normal:

                if (_droneFuel.GetCurrentFuel() <= 0)
                {
                    return;
                }
                if ((_movementDirection != Vector2.zero))
                {
                    _droneFuel.ConsumeFuel();
                }

                if (_movementDirection.y > 0)
                {
                    ApplyUpForce();
                }
                if (_movementDirection.x < 0)
                {
                    ApplyLeftYaw();

                }
                if (_movementDirection.x > 0)
                {
                    ApplyRightYaw();
                }
                break;
            case DroneState.GameOver:
                break;
        }

    }

    private void ApplyUpForce()
    {
        _droneRigidbody.AddForce(transform.up * _upForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
    private void ApplyRightYaw()
    {
        _droneRigidbody.AddTorque(Vector3.forward * -_sideForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
    private void ApplyLeftYaw()
    {
        _droneRigidbody.AddTorque(Vector3.forward * _sideForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
    private void OnCollisionEnter(Collision collision)
    {
        float landingSpeed = collision.relativeVelocity.magnitude;
        var landingAngleCoef = Vector3.Dot(Vector3.up, transform.up);

        if (landingSpeed > _landingSpeedThreshold || landingAngleCoef < _landingAngleThreshold)
        {
            _droneState = DroneState.GameOver;
            onLandingStateChange?.Invoke(LandingState.Crashed);
            _droneDestructor.Detonate();
            return;
        }

        if (collision.gameObject.TryGetComponent<LandingPad>(out LandingPad landingPad))
        {
            _droneState = DroneState.GameOver;
            onLandingStateChange?.Invoke(LandingState.Landed);
        }
        else
        {
            onLandingStateChange?.Invoke(LandingState.Crashed);
            _droneDestructor.Detonate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PointsPickup>(out PointsPickup pointsPickup))
        {
            int pointsReceived = pointsPickup.GetPoints();
            OnPointsPickupArgs pickupArgs = new()
            {
                pointsAmount = pointsReceived,
                pickupTransform = pointsPickup.transform,
            };

            onPointsPickup?.Invoke(pickupArgs);
            pointsPickup.DestroySelf();
        }
        if (other.TryGetComponent<FuelPickup>(out FuelPickup fuelPickup))
        {
            float fuelReceived = fuelPickup.GetFuel();
            OnFuelPickupArgs pickupArgs = new()
            {
                fuelAmount = fuelReceived,
                pickupTransform = fuelPickup.transform,
            };
            Debug.Log($"Im in {GetType().FullName}. Picked up fuel amount {pickupArgs.fuelAmount}");
            onFuelPickup?.Invoke(pickupArgs);
            fuelPickup.DestroySelf();
        }
    }
}
