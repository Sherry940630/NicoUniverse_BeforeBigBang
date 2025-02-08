using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private GameObject statBarPrefab;

    private Dictionary<string, PlayerStatsUI> statBars = new Dictionary<string, PlayerStatsUI>();

    public void Initialize(PlayerStats playerStats)
    {
        playerStats.OnStatChanged += PlayerStats_OnStatChanged;
    }

    public void CreateStatBar(string statName, float initialValue, float maxValue)
    {
        GameObject newStatBar = Instantiate(statBarPrefab, transform);
        Debug.Log($"Instantiated stat bar for {statName}");

        PlayerStatsUI statBarComponent = newStatBar.GetComponent<PlayerStatsUI>();

        if (statBarComponent == null)
        {
            Debug.LogError("PlayerStatsUI component missing on statBarPrefab!");
            return;
        }

        statBarComponent.SetStat(statName, initialValue, maxValue);

        statBars[statName] = statBarComponent;
    }

    private void PlayerStats_OnStatChanged(object sender, PlayerStats.StatChangedEventArgs e)
    {
        Debug.Log("OnStatChanged FIRED!!!!!!!!!!!!!!!!!!!!!!!");
        UpdateStat(e.statName, e.currentValue, e.maxValue);
    }

    public void UpdateStat(string statName, float currentValue, float maxValue)
    {
        if (statBars.TryGetValue(statName, out PlayerStatsUI statBar))
        {
            Debug.Log($"Updating {statName}: Current = {currentValue}, Max = {maxValue}");
            statBar.SetStat(statName, currentValue, maxValue);
        }
        else
        {
            Debug.LogWarning($"Stat bar for {statName} not found!");
        }
    }
}
