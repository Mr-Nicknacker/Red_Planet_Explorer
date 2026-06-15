using UnityEngine;

public class VisualsSpawner : MonoBehaviour
{
    [Header("Pickup popups")]
    [SerializeField] private Popup _popupPrefab;

    //private void Start()
    //{
    //    DroneController.onPointsPickup += SpawnScorePopup;
    //    DroneController.onFuelPickup += SpawnFuelPopup;
    //}
    //private void SpawnScorePopup(GameObject pickupObject)
    //{
    //    Instantiate(_popupPrefab, pickupObject.transform.position, Quaternion.identity);
    //}
    //private void SpawnFuelPopup(GameObject pickupObject)
    //{
    //    Instantiate(_popupPrefab, pickupObject.transform.position, Quaternion.identity);
    //}
}
