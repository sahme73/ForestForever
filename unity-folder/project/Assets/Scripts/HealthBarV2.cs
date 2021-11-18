using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarV2 : MonoBehaviour {
    public Image HealthSlider;
    public PlayerStats CurrentPlayer;
    private float currentHealth;
    private float maxHealth;

    private void Start() {
        maxHealth = CurrentPlayer.GetMaxHealth();
    }

    private void Update() {
        currentHealth = CurrentPlayer.GetCurrentHealth();
        HealthSlider.fillAmount = currentHealth / maxHealth;
    }
}
