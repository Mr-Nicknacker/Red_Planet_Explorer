using System;
using UnityEngine;

public class DroneFuel : MonoBehaviour
{
    private const float MAX_FUEL =100f;

    [SerializeField] private float _consumptionPerSecond;
    private float _currentFuel;

    public static event Action<float> onFuelChangeNormalized;

    public void ResetFuel()
    {
        _currentFuel = MAX_FUEL;
        onFuelChangeNormalized?.Invoke(_currentFuel / MAX_FUEL);
    }
    public float GetCurrentFuel()
    {
        return _currentFuel;
    }
    public void AddFuel(float amount)
    {
        float absFuel = Mathf.Abs(amount);
        Debug.Log($"Im in {GetType().FullName} fuel. Current fuel is {_currentFuel} and i wanto add {absFuel}");

        _currentFuel = (_currentFuel + absFuel > MAX_FUEL) ? MAX_FUEL : (_currentFuel + absFuel);
        onFuelChangeNormalized?.Invoke(_currentFuel / MAX_FUEL);
    }
    public void ConsumeFuel()
    {
        float consumedFuel;
        if (_currentFuel > 0)
        {
            consumedFuel = _consumptionPerSecond * Time.fixedDeltaTime;
            _currentFuel = (_currentFuel - consumedFuel > 0) ? (_currentFuel - consumedFuel) : 0;
            onFuelChangeNormalized?.Invoke(_currentFuel / MAX_FUEL);
        }
        Debug.Log($"Im in {GetType().FullName} and the current fuel is {_currentFuel}");
    }
}
