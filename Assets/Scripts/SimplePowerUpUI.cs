using UnityEngine;
using TMPro;

public class SimplePowerUpUI : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Asigna esto en el inspector

    void Start()
    {
        if (timerText == null)
        {
            // Intentar buscarlo si no está asignado
            timerText = GetComponentInChildren<TextMeshProUGUI>();
        }
        Hide();
    }

    public void SetText(string text)
    {
        if (timerText != null)
        {
            timerText.text = text;
        }
    }

    public void Show()
    {
        if (timerText != null) timerText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (timerText != null) timerText.gameObject.SetActive(false);
    }
}
