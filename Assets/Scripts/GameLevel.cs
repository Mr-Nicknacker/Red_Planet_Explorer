using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private Transform _droneSpawnPoint;
    [SerializeField] private Transform _cameraStartingTransform;
    [SerializeField] private float _zoomedOutCameraDistance;

    public Vector3 GetDroneSpawnPosition()
    {
        return _droneSpawnPoint.position;
    }
    public Transform GetCameraStartingTransform()
    {
        return _cameraStartingTransform;
    }
    public float GetZoomedOutCameraDistance()
    {
        return _zoomedOutCameraDistance;
    }

}
