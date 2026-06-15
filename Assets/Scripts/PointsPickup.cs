using UnityEngine;

public class PointsPickup : MonoBehaviour, IPickup
{
    [SerializeField] private int _pointsToGive;
    [SerializeField] private GameObject _popupObject;
    public int GetPoints()
    {
        return _pointsToGive;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
