using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI statNameText;

    public void Initialize(string statName)
    {
        if (statNameText != null) statNameText.text = statName;
    }

    public void UpdateStat(float currentValue, float maxValue)
    {
        barImage.fillAmount = Mathf.Clamp01( (100-currentValue) / maxValue );
    }
}
