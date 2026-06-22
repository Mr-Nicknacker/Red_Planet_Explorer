using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private int _scoreMultiplier=1;

    public int GetScoreMultiplier()
    {
        return _scoreMultiplier;
    }
}
