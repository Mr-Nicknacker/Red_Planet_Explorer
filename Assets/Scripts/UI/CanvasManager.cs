using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("UI Canvases")]
    [SerializeField] private GameObject _victoryWindowCanvas;
    [SerializeField] private GameObject _defeatWindowPanel;
    [SerializeField] private GameObject _defeatWindowCanvas;
    [SerializeField] private GameObject _pauseWindowCanvas;
    [SerializeField] private GameObject _gameHUD;
    [Header("UI Buttons")]
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeGame;
    [SerializeField] private Button _nextLevelButton;
    [Header("Window texts")]
    [SerializeField] private TextMeshProUGUI _victoryWindowScoreText;
    [SerializeField] private TextMeshProUGUI _defeatWindowScoreText;
    [Header("Delay to show a screen")]

    private bool _isPausable = true;

    public event Action onNextLevelButtonClick;

    public void Initialize()
    {
        HandleButtonInput();
        ShowGameWindow();
    }
    private void Start()
    {
        DroneController.onLandingStateChange += Drone_onLandingStateChange;
        PlayerInputListener.onPausePressed += PauseUnpause;
    }

    private void Drone_onLandingStateChange(DroneController.LandingState state)
    {
        switch (state)
        {
            case DroneController.LandingState.Crashed:
                ShowDefeatWindow();
                _isPausable = false;
                break;
            case DroneController.LandingState.Landed:
                ShowVictoryWindow();
                _isPausable = false;
                break;
        }
    }
    private void ShowDefeatWindow()
    {
        float tweenDelay=0.5f;
        float animationTime = 0.15f;

        _defeatWindowScoreText.text = "—чет: " + PlayerScore.GetInstance().GetCurrentScore().ToString();
        _defeatWindowCanvas.SetActive(true);
 
        _defeatWindowPanel.transform.DOScale(1,animationTime).From(0).SetEase(Ease.InCirc).SetDelay(tweenDelay);
    }
    private void ShowVictoryWindow()
    {
        _victoryWindowScoreText.text = "—чет: " + PlayerScore.GetInstance().GetCurrentScore().ToString();
        _victoryWindowCanvas.gameObject.SetActive(true);
    }
    private void ShowGameWindow()
    {
        _gameHUD.SetActive(true);
        _victoryWindowCanvas.gameObject.SetActive(false);
        _defeatWindowCanvas.SetActive(false);
    }
    private void PauseGame()
    {
        Time.timeScale = 0.0f;
        _gameHUD.SetActive(false);
        _victoryWindowCanvas.gameObject.SetActive(false);
        _defeatWindowCanvas.SetActive(false);
        _pauseWindowCanvas.SetActive(true);
    }
    private void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        _gameHUD.SetActive(true);
        _pauseWindowCanvas.SetActive(false);
    }
    private void PauseUnpause()
    {
        if (Time.timeScale > 0 && _isPausable)
        {
            PauseGame();
        }
        else
        {
            UnPauseGame();
        }
    }
    private void NotifyNextLevelButtonPressed()
    {
        onNextLevelButtonClick?.Invoke();
    }
    private void HandleButtonInput()
    {
        _nextLevelButton.onClick.AddListener(NotifyNextLevelButtonPressed);
        _pauseButton.onClick.AddListener(PauseGame);
        _resumeGame.onClick.AddListener(UnPauseGame);
    }
    private void OnDestroy()
    {
        DroneController.onLandingStateChange -= Drone_onLandingStateChange;
        PlayerInputListener.onPausePressed -= PauseUnpause;
        StopAllCoroutines();
    }
}
