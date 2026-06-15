using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputListener
{
    private DroneInputActions _inputActions;

    public static event Action onPausePressed;  

    private static PlayerInputListener _instance;

    public static PlayerInputListener GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PlayerInputListener();
        }
        return _instance;
    }
    public void Initialize()
    {
        _inputActions = new DroneInputActions();
        _inputActions.Enable();
        _inputActions.Player.Pause.performed += PausePerformed;
    }

    private void PausePerformed(InputAction.CallbackContext context)
    {
        onPausePressed?.Invoke();
    }
    public Vector2 GetMovementVector2()
    {
        Vector2 movementDirection = _inputActions.Player.Movement.ReadValue<Vector2>();
        return movementDirection;        
}
    public void DisableListener()
    {
        _inputActions.Disable();
    }

}
