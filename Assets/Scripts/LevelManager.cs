using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerDrone;
    [SerializeField] private GameLevel[] _levelPrefabs;
    [SerializeField] private CinemachineCamera _camera;

    private static int levelNumber = 0;
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
    private void LoadCurrentLevel()
    {
        _currentLevel = Instantiate(_levelPrefabs[levelNumber], Vector3.zero, Quaternion.identity);
        _playerDrone.transform.position = _currentLevel.GetDroneSpawnPosition();
        //_camera.Target.TrackingTarget = _currentLevel.GetCameraStartingTransform();
    }
    //To progress to next level. Should activate on button press
    public void LoadNextLevel()
    {
        levelNumber++;
        LoadCurrentLevel();
    }
    public void LoadFirstLevel()
    {
        levelNumber = 0;
        LoadCurrentLevel();
    }
    public bool IsLastLevel()
    {
        return levelNumber < _levelPrefabs.Length - 1;
    }
    private void OnDestroy()
    {
        DroneController.onDroneStateChange -= ZoomIn;
    }

}
