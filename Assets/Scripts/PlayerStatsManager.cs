using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private List<PlayerStatsUI> statBarList;

    private void Start()
    {
        playerStats.OnStatChanged += UpdateStatBar;
        InitializeStatBars();
    }

    private void InitializeStatBars()
    {
        foreach (var statBar in statBarList)
        {
            string statName = statBar.name.Replace("Bar", "").Trim();
            statBar.Initialize(statName);
        }
    }

    private void UpdateStatBar(string statName, float currentValue, float maxValue)
    {
        foreach (var statBar in statBarList)
        {
            if (statBar.name.StartsWith(statName))
            {
                statBar.UpdateStat(currentValue, maxValue);
                Debug.Log("BAR: " + statName + " = " + currentValue + " / " + maxValue);
                break;
            }
        }
    }
}
