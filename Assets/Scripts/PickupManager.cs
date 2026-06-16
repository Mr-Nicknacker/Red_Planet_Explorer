using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private DroneFuel _droneFuel;

    private void Start()
    {
        DroneController.onPointsPickup += AddPoints;
        DroneController.onFuelPickup += AddFuel;        
    }

    private void AddFuel(DroneController.OnFuelPickupArgs args)
    {
        _droneFuel.AddFuel(args.fuelAmount);
        Debug.Log($"I am in {GetType().FullName} and adding {args.fuelAmount} fuel and now i'm at {_droneFuel.GetCurrentFuel()}");
    }

    private void AddPoints(DroneController.OnPointsPickupArgs args)
    {
        PlayerScore.GetInstance().AddScore(args.pointsAmount);
    }

    private void OnDisable()
    {
        DroneController.onPointsPickup -= AddPoints;
        DroneController.onFuelPickup -= AddFuel;
    }
}
