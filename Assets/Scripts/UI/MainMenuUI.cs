using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private Button _startGameButton;
    private void Awake()
    {
        HandleButtonInput();
    }
    private void StartGame()
    {
        //GameManager._instance.StartGame();
        SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
        GameManager.Instance.LoadFirstLevel();
        PlayerScore.GetInstance().ResetScore();
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    private void HandleButtonInput()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _quitGameButton.onClick.AddListener(QuitGame);
    }
}
