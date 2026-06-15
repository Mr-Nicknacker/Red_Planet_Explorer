using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    private void Awake()
    {
        PlayerInputListener.GetInstance().Initialize();
    }
    private void OnDestroy()
    {
        PlayerInputListener.GetInstance().DisableListener();
    }
}
