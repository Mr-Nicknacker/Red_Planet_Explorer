using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreValue;
    [SerializeField] private TextMeshProUGUI _levelNumber;
    [SerializeField] private Image _fuelBarFill;
    [Header("Fuel bar colors")]
    [SerializeField] private Color _fullFuelColor;
    [SerializeField] private Color _twoThirdsFuelColor;
    [SerializeField] private Color _thirdFuelColor;

    private void Update()
    {
        DisplayLevel(GameManager.Instance.GetLevelNumber() + 1);
        DisplayFuel(DroneFuel.Instance.GetFuelNormalized());
        DisplayPoints(PlayerScore.GetInstance().GetCurrentScore());
    }

    private void DisplayLevel(int level)
    {
        _levelNumber.text = level.ToString();
    }

    private void DisplayFuel(float normalizedFuel)
    {
        _fuelBarFill.fillAmount = normalizedFuel;
        ChangeFuelBarFillColor(normalizedFuel);
    }
    private void DisplayPoints(int points)
    {
        _scoreValue.text = points.ToString();
    }
    private void ChangeFuelBarFillColor(float fuelRemaining)
    {

        float twoThirdsTank = 2 / 3f;
        float thirdTank = 1 / 3f;

        if (fuelRemaining > twoThirdsTank)
        {
            _fuelBarFill.color = _fullFuelColor;
        }
        if (fuelRemaining <= twoThirdsTank)
        {
            _fuelBarFill.color = _twoThirdsFuelColor;
        }
        if (fuelRemaining <= thirdTank)
        {
            _fuelBarFill.color = _thirdFuelColor;
        }
    }
}
