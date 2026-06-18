using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _gameOverText;
    private void Awake()
    {
        _gameOverText.text = "Общий счет: "+PlayerScore.GetInstance().GetTotalScore().ToString();
    }
}
