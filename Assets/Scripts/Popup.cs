using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField]private TextMeshPro _textField;
    private void OnEnable()
    {
        SetText("+100");
        Destroy(gameObject,1.5f);
    }
    public void SetText(string text)
    {
        _textField.text = text;
    }
}
