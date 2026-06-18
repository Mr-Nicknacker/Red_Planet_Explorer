using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private Transform _droneSpawnPoint;
    [SerializeField] private Transform _cameraStartingTransform;

    public Vector3 GetDroneSpawnPosition()
    {
        return _droneSpawnPoint.position;
    }
    public Transform GetCameraStartingTransform()
    {
        return _cameraStartingTransform;
    }

}
