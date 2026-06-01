using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private GameObject _victoryWindowCanvas;
    [SerializeField] private GameObject _defeatWindowCanvas;

    [SerializeField] private Button _retryButton;

    private void Awake()
    {
        HandleButtonInput();
    }
    private void Start()
    {
        DroneController.onLandingStateChange += Drone_onLandingStateChange;
    }

    private void Drone_onLandingStateChange(DroneController.LandingState state)
    {
        Debug.Log(state);
        switch (state)
        {
            case DroneController.LandingState.Crashed:
                ShowDefeatWindow();
                break;
            case DroneController.LandingState.Landed:
                ShowVictoryWindow();
                break;
        }
    }

    private void ShowDefeatWindow()
    {
        _victoryWindowCanvas.SetActive(false);
        _defeatWindowCanvas.SetActive(true);
    }
    private void ShowVictoryWindow()
    {
        _victoryWindowCanvas.SetActive(true);
        _defeatWindowCanvas.SetActive(false);
    }
    private void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    private void HandleButtonInput()
    {
        _retryButton.onClick.AddListener(ResetGame);
    }
}
