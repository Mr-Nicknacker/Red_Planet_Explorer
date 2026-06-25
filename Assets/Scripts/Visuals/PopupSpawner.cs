using UnityEngine;
using UnityEngine.VFX;

public class PopupSpawner : MonoBehaviour
{
    [Header("Pickup popups")]
    [SerializeField] private Popup _popupPrefab;

    private void Start()
    {
        DroneController.onPointsPickup += SpawnScorePopup;
        DroneController.onFuelPickup += SpawnFuelPopup;
    }

    private void SpawnFuelPopup(DroneController.OnFuelPickupArgs args)
    {
        Instantiate(_popupPrefab, args.pickupTransform.position, Quaternion.identity).SetText("+FUEL");
    }

    private void SpawnScorePopup(DroneController.OnPointsPickupArgs args)
    {
        Instantiate(_popupPrefab, args.pickupTransform.position, Quaternion.identity).SetText($"+{args.pointsAmount}");
    }
}
