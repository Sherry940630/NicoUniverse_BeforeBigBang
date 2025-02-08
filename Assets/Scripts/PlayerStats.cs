using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public event Action<string, float, float> OnStatChanged;

    private float hunger = 0f;
    private float thirst = 0f;
    private float boredom = 0f;

    private const float MAX_HUNGER = 100f;
    private const float MAX_THIRST = 100f;
    private const float MAX_BOREDOM = 100f;

    private void Start()
    {
        NotifyStatChange("Hunger", hunger, MAX_HUNGER);
        NotifyStatChange("Thirst", thirst, MAX_THIRST);
        NotifyStatChange("Boredom", boredom, MAX_BOREDOM);
    }

    private void Update()
    {
        ChangeStat("Hunger", ref hunger, MAX_HUNGER, Time.deltaTime * 1f);
        ChangeStat("Thirst", ref thirst, MAX_THIRST, Time.deltaTime * 2f);
        ChangeStat("Boredom", ref boredom, MAX_BOREDOM, Time.deltaTime * 0.5f);
    }

    private void ChangeStat(string statName, ref float statValue, float maxValue, float delta)
    {
        statValue = Mathf.Clamp(statValue + delta, 0, maxValue);
        NotifyStatChange(statName, statValue, maxValue);
    }

    private void NotifyStatChange(string statName, float currentValue, float maxValue)
    {
        OnStatChanged?.Invoke(statName, currentValue, maxValue);
    }
}
