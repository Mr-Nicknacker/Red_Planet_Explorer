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
        SceneLoader.LoadScene(SceneLoader.SceneName.GameScene);
        PlayerScore.GetInstance().ResetTotalScore();
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
