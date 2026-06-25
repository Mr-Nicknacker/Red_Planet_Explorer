using Unity.Cinemachine;
using UnityEngine;

public class CameraShake:MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    [SerializeField] float number=8f;
    private void Start()
    {
        DroneController.onLandingStateChange += ShakeCamera;
    }

    private void ShakeCamera(DroneController.LandingState state)
    {
        if (state == DroneController.LandingState.Crashed)
        {
            _impulseSource.GenerateImpulse(number);
        }
    }
    private void OnDestroy()
    {
        DroneController.onLandingStateChange -= ShakeCamera;
    }
}
