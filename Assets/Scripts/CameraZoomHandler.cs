using Unity.Cinemachine;
using UnityEngine;

public class CameraZoomHandler : MonoBehaviour
{
    private const float DEFAULT_ZOOM = 40f;
    private float _targetCameraDistance;

    [SerializeField]private CinemachinePositionComposer _cameraComposer;
    [SerializeField]private float _zoomSpeed=5f;

    private void Update()
    {
        _cameraComposer.CameraDistance = Mathf.Lerp(_cameraComposer.CameraDistance, _targetCameraDistance, Time.deltaTime*_zoomSpeed);
    }
    public void ZoomCamera(float targetCameraDistance)
    {
        _targetCameraDistance = targetCameraDistance;
    }
    public void ResetZoom()
    {
        ZoomCamera(DEFAULT_ZOOM);
    }
}
