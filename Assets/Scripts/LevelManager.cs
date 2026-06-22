using Unity.Cinemachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerDrone;
    [SerializeField] private GameLevel[] _levelPrefabs;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private CameraZoomHandler _cameraZoomHandler;

    private static int _levelNumber = 0;
    private GameLevel _currentLevel;    

    private void Start()
    {
        DroneController.onDroneStateChange += ZoomIn;        
    }

    private void ZoomIn(DroneController.DroneState state)
    {
        if (state == DroneController.DroneState.Operating)
        {
            _cameraZoomHandler.ResetZoom();
            _camera.Target.TrackingTarget = _playerDrone.transform;
        }
    }
    
    public void LoadCurrentLevel()
    {
        _currentLevel = Instantiate(_levelPrefabs[_levelNumber], Vector3.zero, Quaternion.identity);        
        _playerDrone.gameObject.transform.position = _currentLevel.GetDroneSpawnPosition();        
        _camera.Target.TrackingTarget = _currentLevel.GetCameraStartingTransform();
        _cameraZoomHandler.ZoomCamera(_currentLevel.GetZoomedOutCameraDistance());
    }
    public void LoadNextLevel()
    {
        _levelNumber++;
        LoadCurrentLevel();
    }
    public int GetLevelNumber()
    {
        return _levelNumber;
    }
    public bool IsLastLevel()
    {
        return _levelNumber >= _levelPrefabs.Length - 1;
    }
    private void OnDestroy()
    {
        DroneController.onDroneStateChange -= ZoomIn;
    }

}
