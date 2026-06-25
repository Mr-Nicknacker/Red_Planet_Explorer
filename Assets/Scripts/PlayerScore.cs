using System;
using UnityEngine;

public class PlayerScore
{
    
    private int _currentScore=0;
    private static int _totalScore=0;

    private static PlayerScore _instance;
    public static PlayerScore GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PlayerScore();
        }
        return _instance;
    }
    public void ResetTotalScore()
    {
        _totalScore = 0;
    }
    public void ResetCurrentScore()
    {
        _currentScore = 0;
    }
    public int GetTotalScore()
    {
        return _totalScore;
    }
    public int GetCurrentScore()
    {
        return _currentScore;
    }
    public void AddScore(int amount)
    {
        int absScore = Mathf.Abs(amount);
        _currentScore += absScore;
    }
    public void ComputeMultiplier(int multiplier)
    {
        int absMultiplier = Mathf.Abs(multiplier);
        _currentScore *= absMultiplier;
        _totalScore += _currentScore;
    }

}
