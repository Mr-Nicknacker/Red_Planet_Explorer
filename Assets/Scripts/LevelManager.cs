using Unity.Cinemachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerDrone;
    [SerializeField] private GameLevel[] _levelPrefabs;
    [SerializeField] private CinemachineCamera _camera;

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
            //set camera gaming position
            Debug.Log("zooming....");
        }
    }

    //To load a level when starting a new game
    public void LoadCurrentLevel()
    {
        _currentLevel = Instantiate(_levelPrefabs[_levelNumber], Vector3.zero, Quaternion.identity);
        _playerDrone.transform.position = _currentLevel.GetDroneSpawnPosition();
        //_camera.Target.TrackingTarget = _currentLevel.GetCameraStartingTransform();
    }
    //To progress to next level. Should activate on button press
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
