using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float health = 0.0f;
    [SerializeField] private float maxHealth = 100.0f;

    private float speed = 0.0f;
    public float setSpeed = 10.0f;

    private void Start() {
        health = maxHealth;
        speed = setSpeed;
    }

    public void UpdateHealth(float mod) {
        health += mod;
        if (health > maxHealth) {
            health = maxHealth;
        } else if (health < 0.0f) {
            health = 0.0f;
            Debug.Log("respawn");
        }

    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public float GetSpeed() {
        return speed;
    }

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 1500, 100), "Health: " + health.ToString());
    }
}
