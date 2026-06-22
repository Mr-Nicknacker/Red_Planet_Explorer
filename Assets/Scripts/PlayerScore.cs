using System;
using UnityEngine;

public class PlayerScore
{
    
    private int _currentScore;
    private static int _totalScore=0;

    public event Action<int> onScoreChange;

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
        Debug.Log($"{GetType().FullName} - total score = {_totalScore}. reset");
    }
    public int GetTotalScore()
    {
        return _totalScore;
    }
    public void AddScore(int amount)
    {
        int absScore = Mathf.Abs(amount);
        _currentScore += absScore;
        onScoreChange?.Invoke(_currentScore);
        Debug.Log($"Current score is { _currentScore}");
    }
    public void ComputeMultiplier(int multiplier)
    {
        int absMultiplier = Mathf.Abs(multiplier);
        _currentScore *= absMultiplier;
        _totalScore += _currentScore;
        onScoreChange?.Invoke(_currentScore);
        Debug.Log($"current score is {_currentScore}");
        Debug.Log($"total score is {_totalScore}");
    }

}
