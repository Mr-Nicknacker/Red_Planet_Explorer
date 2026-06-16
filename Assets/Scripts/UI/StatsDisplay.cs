using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statsNumbers;
    [SerializeField] private Image _fuelBarFill;
    [Header("Fuel bar colors")]
    [SerializeField] private Color _fullFuelColor;
    [SerializeField] private Color _twoThirdsFuelColor;
    [SerializeField] private Color _thirdFuelColor;
    
    void Start()
    {
        DroneFuel.onFuelChangeNormalized += DisplayFuel;
        PlayerScore.onScoreChange += DisplayPoints;
    }

    private void DisplayFuel(float normalizedFuel)
    {
        _fuelBarFill.fillAmount = normalizedFuel;
        //Debug.Log("current fuel on display: "+fuel);
        ChangeFuelBarFillColor(normalizedFuel);
    }
    private void DisplayPoints(int points)
    {
        _statsNumbers.text =$"{points}";
    }
    private void OnDisable()
    {
        DroneFuel.onFuelChangeNormalized -= DisplayFuel;
        PlayerScore.onScoreChange -= DisplayPoints;
    }
    private void ChangeFuelBarFillColor(float fuelRemaining)
    {
        if (fuelRemaining > 2/3)
        {
            _fuelBarFill.color = _fullFuelColor;
        }
        if (fuelRemaining <= 2/3)
        {
            _fuelBarFill.color = _twoThirdsFuelColor;
        }
        if (fuelRemaining <= 1/3)
        {
            _fuelBarFill.color = _thirdFuelColor;
        }
    }
}
