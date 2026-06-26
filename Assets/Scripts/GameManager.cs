using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager _canvasManager;
    
    public static GameManager Instance { get; private set; }

    public void Initialize()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
        _canvasManager.Initialize();
    }
    private void Start()
    {
        RetryButtonNotifier.OnRetryButtonClick += RetryLevel;
        ToMainMenuButtonNotifier.OnToMainMenuButtonClick += BackToMainMenu;
        _canvasManager.onNextLevelButtonClick += LoadNextLevel;

        LevelManager.Instance.LoadCurrentLevel();
        StartLevel();
    }
    private void StartLevel()
    {
        PlayerScore.GetInstance().ResetCurrentScore();
        DroneFuel.Instance.ResetFuel();
        Time.timeScale = 1f;
    }
    private void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
    }
    private void LoadNextLevel()
    {
        if (LevelManager.Instance.IsLastLevel())
        {
            SceneLoader.LoadScene(SceneLoader.SceneName.GameOverScene);            
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
            LevelManager.Instance.LoadNextLevel();
            StartLevel();
        }  
    }
    private void BackToMainMenu()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.MainMenuScene);
    }
    public void ResetToFirstLevel()
    {
        LevelManager.Instance.ResetLevels();
    }
    public int GetLevelNumber()
    {
        return LevelManager.Instance.GetLevelNumber();
    }
}
