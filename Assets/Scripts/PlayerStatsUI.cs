using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI statNameText;

    [SerializeField] private PlayerStats playerStats;


    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        //barImage.fillAmount = Mathf.Clamp01(playerStats.GetHunger() / 100);
    }

    public void SetStat(string statName, float currentValue, float maxValue)
    {
        if (statNameText != null)
            statNameText.text = statName;

        barImage.fillAmount = Mathf.Clamp01(currentValue / maxValue);
        Debug.Log($"{statName}: Calculated Fill Amount = {Mathf.Clamp01(currentValue / maxValue)}, Current Fill Amount = {barImage.fillAmount}");
        Debug.Log($"{statName} - Current: {currentValue}, Max: {maxValue}, Fill: {barImage.fillAmount}");
    }
}
