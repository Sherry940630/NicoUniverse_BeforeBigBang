using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsManager statBarManager;

    [SerializeField] private float hunger = 50f;
    [SerializeField] private float thirst = 50f;
    [SerializeField] private float boredom = 50f;

    [SerializeField] private float maxHunger = 100;
    [SerializeField] private float maxThirst = 100;
    [SerializeField] private float maxBoredom = 100;

    public event EventHandler<StatChangedEventArgs> OnStatChanged;
    public class StatChangedEventArgs : EventArgs
    {
        public string statName;
        public float currentValue;
        public float maxValue;
    }

    private void Start()
    {
        maxHunger = 100;
        maxThirst = 100;
        maxBoredom = 100;
        statBarManager.Initialize(this);
        statBarManager.CreateStatBar("Hunger", hunger, maxHunger);
        statBarManager.CreateStatBar("Thirst", thirst, maxThirst);
        statBarManager.CreateStatBar("Boredom", boredom, maxBoredom);
    }

    private void Update()
    {
        hunger = Mathf.Clamp(hunger + Time.deltaTime * 1f, 0, maxHunger);
        thirst = Mathf.Clamp(thirst + Time.deltaTime * 2f, 0, maxThirst);
        boredom = Mathf.Clamp(boredom + Time.deltaTime * 0.5f, 0, maxBoredom);

        TriggerStatChange("Hunger", hunger, maxHunger);
        TriggerStatChange("Thirst", thirst, maxThirst);
        TriggerStatChange("Boredom", boredom, maxBoredom);

        statBarManager.UpdateStat("Hunger", hunger, maxHunger);
        statBarManager.UpdateStat("Thirst", thirst, maxThirst);
        statBarManager.UpdateStat("Boredom", boredom, maxBoredom);
    }

    private void TriggerStatChange(string localStatName, float localCurrentValue, float localMaxValue)
    {
        Debug.Log($"Stat Changed - {localStatName}: {localCurrentValue}/{localMaxValue}");

        if (OnStatChanged != null)
        {
            OnStatChanged?.Invoke(this, new StatChangedEventArgs
            {
                statName = localStatName,
                currentValue = localCurrentValue,
                maxValue = localMaxValue
            });
        }
        else
        {
            Debug.LogWarning($"No listeners for stat {localStatName}");
        }
    }
    
    public float GetHunger()
    {
        return hunger;
    }

}
