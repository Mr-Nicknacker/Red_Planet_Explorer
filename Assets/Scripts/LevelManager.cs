using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerDrone;
    [SerializeField] private GameLevel[] _levelPrefabs;
    [SerializeField] private CinemachineCamera _camera;

    private int _levelNumber = 0;
    private GameLevel _currentLevel;

    //To load a level when starting a new game
    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(1);
        _currentLevel = Instantiate(_levelPrefabs[levelNumber], Vector3.zero, Quaternion.identity);
        _playerDrone.transform.position = _currentLevel.GetDroneSpawnPosition();
    }
    //To progress to next level. Should activate on button press
    public void LoadNextLevel()
    {
        _levelNumber++;
        if (_levelNumber < _levelPrefabs.Length)
        {            
            SceneManager.LoadScene(1);
            _currentLevel = Instantiate(_levelPrefabs[_levelNumber], Vector3.zero, Quaternion.identity);
            _playerDrone.transform.position = _currentLevel.GetDroneSpawnPosition();
        }
        else
        {
            SceneManager.LoadScene(null); // load game over scene with a total score
        }
    }
    private void ZoomOut(){}
}
