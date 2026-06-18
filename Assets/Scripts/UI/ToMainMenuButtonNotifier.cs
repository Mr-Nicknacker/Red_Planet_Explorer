using System;
using UnityEngine;
using UnityEngine.UI;

public class ToMainMenuButtonNotifier : MonoBehaviour
{
    private Button _mainMenuButton;
    public static event Action OnToMainMenuButtonClick;

    void Start()
    {
        _mainMenuButton= GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(SendMainMenuEvent);
    }

    private void SendMainMenuEvent()
    {
        OnToMainMenuButtonClick?.Invoke();
    }


}
