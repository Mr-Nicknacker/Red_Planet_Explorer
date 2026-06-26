using UnityEngine;

public class DroneFuel : MonoBehaviour
{
    [SerializeField] private float _maxFuel = 100f;
    [SerializeField] private float _consumptionPerSecond;
    private float _currentFuel;

    public static DroneFuel Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
    }
    public void ResetFuel()
    {
        _currentFuel = _maxFuel;
    }
    public float GetCurrentFuel()
    {
        return _currentFuel;
    }
    public float GetFuelNormalized()
    {
        return _currentFuel / _maxFuel;
    }
    public void AddFuel(float amount)
    {
        float absFuel = Mathf.Abs(amount);

        _currentFuel = (_currentFuel + absFuel > _maxFuel) ? _maxFuel : (_currentFuel + absFuel);
    }
    public void ConsumeFuel()
    {
        float consumedFuel;
        if (_currentFuel > 0)
        {
            consumedFuel = _consumptionPerSecond * Time.fixedDeltaTime;
            _currentFuel = (_currentFuel - consumedFuel > 0) ? (_currentFuel - consumedFuel) : 0;
        }
    }
}
