using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField]private DroneController _droneController;
    private void Awake()
    {
        PlayerInputListener.GetInstance().Initialize();
        _droneController.Initialize();
    }
    private void OnDestroy()
    {
        PlayerInputListener.GetInstance().DisableListener();
    }
}
