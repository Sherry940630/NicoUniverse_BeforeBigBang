using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float hunger = 0;

    public event EventHandler<OnPlayerHungerChangedEventArgs> OnPlayerHungerChanged;
    public class OnPlayerHungerChangedEventArgs : EventArgs
    {
        public float hungerNormalized;
    }

    private void Update()
    {
        if (hunger <= 50)
        {
            hunger += Time.deltaTime * 1;
            OnPlayerHungerChanged?.Invoke(this, new OnPlayerHungerChangedEventArgs { hungerNormalized = hunger/50 });
        }
    }
}
