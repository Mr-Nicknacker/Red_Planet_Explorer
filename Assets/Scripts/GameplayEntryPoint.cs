using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField]private GameManager _gameManager;
    [SerializeField]private DroneController _droneController;
    [SerializeField]private DroneFuel _droneFuel;
    [SerializeField]private LevelManager _levelManager;
    private void Awake()
    {
        PlayerInputListener.GetInstance().Initialize();
        _gameManager.Initialize();
        _droneController.Initialize();
        _droneFuel.Initialize();
        _levelManager.Initialize();
    }
    private void OnDestroy()
    {
        PlayerInputListener.GetInstance().DisableListener();
    }
}
