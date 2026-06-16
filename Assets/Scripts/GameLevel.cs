using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private Transform _droneSpawnPoint;
    //Хьюго перезагружает сцену и дрон спавнится сам. Не надо его инстанциировать
    public Vector3 GetDroneSpawnPosition()
    {
        return _droneSpawnPoint.position;
    }

}
