using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private LevelManager _levelManager;
    
    
    public static GameManager Instance { get; private set; }

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
        RetryButtonNotifier.OnRetryButtonClick += RetryLevel;
        ToMainMenuButtonNotifier.OnToMainMenuButtonClick += BackToMainMenu;
        CanvasManager.onNextLevelButtonClick += LoadNextLevel;
        StartGame();
    }
    public void StartGame()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
        LoadFirstLevel();        
        PlayerScore.GetInstance().ResetScore();
    }
    public void LoadFirstLevel()
    {
        _levelManager.LoadFirstLevel();
    }
    private void RetryLevel()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
        //time.timescale=1;
    }
    private void LoadNextLevel()
    {
        if (!_levelManager.IsLastLevel())
        {
            SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
            _levelManager.LoadNextLevel();
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.SceneName.GameOverScene);
        }  
    }
    private void BackToMainMenu()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.MainMenuScene);
    }

}
