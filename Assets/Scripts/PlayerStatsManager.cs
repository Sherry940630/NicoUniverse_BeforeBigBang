using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private List<PlayerStatsUI> statBars; // Assigned in Inspector

    private void Start()
    {
        playerStats.OnStatChanged += UpdateStatBar;
        InitializeStatBars();
    }

    private void InitializeStatBars()
    {
        foreach (var statBar in statBars)
        {
            string statName = statBar.name.Replace("Bar", "").Trim(); // Ensure proper names
            statBar.Initialize(statName);
        }
    }

    private void UpdateStatBar(string statName, float currentValue, float maxValue)
    {
        foreach (var statBar in statBars)
        {
            if (statBar.name.StartsWith(statName))
            {
                statBar.UpdateStat(currentValue, maxValue);
                break;
            }
        }
    }
}
