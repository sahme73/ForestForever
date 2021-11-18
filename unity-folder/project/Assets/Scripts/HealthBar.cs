using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image HealthSlider;
    private PlayerStats CurrentPlayer;
    private float currentHealth;
    private float maxHealth;

    private void Start() {

        if (GetComponent<PlayerStats>() != null) {
            CurrentPlayer = GetComponent<PlayerStats>();
            maxHealth = CurrentPlayer.GetMaxHealth();
        }

    }

    private void Update() {
        if (CurrentPlayer == null) {
            return;
        }

        currentHealth = CurrentPlayer.GetCurrentHealth();
        HealthSlider.fillAmount = currentHealth / maxHealth;
    }
}
