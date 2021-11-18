using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float health = 0.0f;
    private float maxHealth = 100.0f;

    private float speed = 0.0f;
    public float setSpeed = 10.0f;
    public float setMaxHealth = 100.0f;

    private void Start() {
        //intializing variables
        maxHealth = setMaxHealth;
        speed = setSpeed;

        health = maxHealth;
    }

    public void UpdateHealth(float mod) {
        health += mod;
        if (health > maxHealth) {
            health = maxHealth;
        } else if (health <= 0.0f) {
            health = 0.0f;
            Respawn();
        }
    }

    private void Respawn() {
        SetSpeed(0.0f);
        Debug.Log("YOU DIED");
        GetComponent<Rigidbody2D>().position = new Vector2(0.0f, 0.0f);
        Invoke("RespawnComplete", 3.0f);
    }

    private void RespawnComplete() {
        SetSpeed(setSpeed);
        health = maxHealth;
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public float GetSpeed() {
        return speed;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetCurrentHealth() {
        return health;
    }

    void OnGUI() {
        GUI.Label(new Rect(16, 14, 1500, 100), "Health: " + health.ToString());
    }
}
