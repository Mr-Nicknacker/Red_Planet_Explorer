using TMPro;
using UnityEngine;

//The scrip is located on a Lander Panel prefab
public class ScoreMultiplierDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro _multiplierText;
    private int _scoreMultiplier;

    private void Awake()
    {
        _scoreMultiplier = GetComponent<LandingPad>().GetScoreMultiplier();
        SetMultiplierText(_scoreMultiplier);
    }
    private void SetMultiplierText(int multiplierValue)
    {
        _multiplierText.text = "x"+multiplierValue.ToString();
    }

}
