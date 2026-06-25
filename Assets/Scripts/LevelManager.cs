using System;
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

    public event Action<int> onLevelChange;
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
    }
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
        _playerDrone.transform.position = _currentLevel.GetDroneSpawnPosition();        
        _camera.Target.TrackingTarget = _currentLevel.GetCameraStartingTransform();
        _cameraZoomHandler.ZoomCamera(_currentLevel.GetZoomedOutCameraDistance());
        onLevelChange?.Invoke(_levelNumber);
    }
    public void LoadNextLevel()
    {
        _levelNumber++;
    }
    public int GetLevelNumber()
    {
        return _levelNumber;
    }
    public bool IsLastLevel()
    {
        return _levelNumber >= _levelPrefabs.Length - 1;
    }
    public void ResetLevels()
    {
        _levelNumber = 0;
        Debug.Log("Resetting levels");
        onLevelChange?.Invoke(_levelNumber);
    }
    private void OnDestroy()
    {
        DroneController.onDroneStateChange -= ZoomIn;
    }

}
