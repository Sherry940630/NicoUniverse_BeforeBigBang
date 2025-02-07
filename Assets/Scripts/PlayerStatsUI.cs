using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private PlayerStats hunger;
    [SerializeField] private Image barImage;

    private void Start()
    {
       hunger.OnPlayerHungerChanged += PlayerStats_OnPlayerHungerChanged;
        barImage.fillAmount = 0f;
    }

    private void PlayerStats_OnPlayerHungerChanged(object sender, PlayerStats.OnPlayerHungerChangedEventArgs e)
    {
        barImage.fillAmount = e.hungerNormalized;
    }
}
