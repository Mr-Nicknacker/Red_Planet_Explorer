using System;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField] private float _upForce;
    [SerializeField] private float _sideForce;
    [SerializeField] private float _landingSpeedThreshold;
    [SerializeField] private float _landingAngleThreshold;

    private Rigidbody _droneRigidbody;
    private DroneDestructor _droneDestructor;
    private DroneState _droneState;
    private Vector2 _movementDirection;

    public enum DroneState
    {
        WatingToStart,
        Operating,
        GameOver
    }
    public enum LandingState
    {
        Crashed,
        Landed
    }
        
    public static event Action<DroneState> onDroneStateChange;
    public static event Action<LandingState> onLandingStateChange;
    public static event Action<OnPointsPickupArgs> onPointsPickup;
    public static event Action<OnFuelPickupArgs> onFuelPickup;
    public static event Action onForceUp;
    public static event Action onForceLeft;
    public static event Action onForceRight;
    public static event Action onForceNone;

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

    public void Initialize()
    {
        _droneRigidbody = GetComponent<Rigidbody>();
        _droneRigidbody.sleepThreshold = 0f;
        _droneRigidbody.useGravity = false;

        ChangeDroneState(DroneState.WatingToStart); ;

        _droneDestructor = GetComponent<DroneDestructor>();
    }

    private void FixedUpdate()
    {
        _movementDirection = PlayerInputListener.GetInstance().GetMovementVector2();
        onForceNone?.Invoke();

        switch (_droneState)
        {
            case DroneState.WatingToStart:
                if (_movementDirection != Vector2.zero)
                {
                    _droneRigidbody.useGravity = true;
                    ChangeDroneState(DroneState.Operating);
                }
                break;
            case DroneState.Operating:

                if (DroneFuel.Instance.GetCurrentFuel() <= 0)
                {
                    return;
                }
                if ((_movementDirection != Vector2.zero))
                {
                    DroneFuel.Instance.ConsumeFuel();
                }

                if (_movementDirection.y > 0)
                {
                    ApplyUpForce();
                    onForceUp?.Invoke();
                }
                if (_movementDirection.x < 0)
                {
                    ApplyLeftYaw();
                    onForceLeft?.Invoke();

                }
                if (_movementDirection.x > 0)
                {
                    ApplyRightYaw();
                    onForceRight?.Invoke();
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
        float landingAngleCoef = Vector3.Dot(Vector3.up, transform.up);
        int scoreMultiplier;

        if (landingSpeed > _landingSpeedThreshold || landingAngleCoef < _landingAngleThreshold)
        {            
            ChangeDroneState(DroneState.GameOver);
            onLandingStateChange?.Invoke(LandingState.Crashed);
            _droneDestructor.Detonate();
            return;
        }

        if (collision.gameObject.TryGetComponent<LandingPad>(out LandingPad landingPad))
        {
            scoreMultiplier = landingPad.GetScoreMultiplier();
            PlayerScore.GetInstance().ComputeMultiplier(scoreMultiplier);
            ChangeDroneState(DroneState.GameOver);
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

            OnPointsPickupArgs pointsPickupArgs = new()
            {
                pointsAmount = pointsReceived,
                pickupTransform = pointsPickup.transform,
            };

            onPointsPickup?.Invoke(pointsPickupArgs);
            pointsPickup.DestroySelf();
        }
        if (other.TryGetComponent<FuelPickup>(out FuelPickup fuelPickup))
        {
            float fuelReceived = fuelPickup.GetFuel();

            OnFuelPickupArgs fuelPickupArgs = new()
            {
                fuelAmount = fuelReceived,
                pickupTransform = fuelPickup.transform,
            };
            onFuelPickup?.Invoke(fuelPickupArgs);
            fuelPickup.DestroySelf();
        }
    }
    private void ChangeDroneState(DroneState newState)
    {
        _droneState = newState;
        onDroneStateChange?.Invoke(newState);
    }
}
