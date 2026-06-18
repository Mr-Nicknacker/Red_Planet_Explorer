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
        _canvasManager.onNextLevelButtonClick += LoadNextLevel;
        _levelManager.LoadCurrentLevel();
    }
    public void StartGame()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
                
        PlayerScore.GetInstance().ResetTotalScore();
    }
    private void RetryLevel()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
        //time.timescale=1;
    }
    private void LoadNextLevel()
    {
        if (_levelManager.IsLastLevel())
        {
            SceneLoader.LoadScene(SceneLoader.SceneName.GameOverScene);            
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
            _levelManager.LoadNextLevel();
        }  
    }
    private void BackToMainMenu()
    {
        SceneLoader.LoadScene(SceneLoader.SceneName.MainMenuScene);
    }

}
