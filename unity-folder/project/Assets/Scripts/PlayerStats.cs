using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, PlayerInterface
{
    public GameObject Logger;
    private float currentHealth = 0.0f;
    private float maxHealth = 100.0f;

    private float speed = 0.0f;
    public float setSpeed = 10.0f;
    public float setMaxHealth = 100.0f;
    public float swingSpeed = 1.0f;

    public float regenPerSecond = 1.0f; //rps

    public float damage = 5.0f; //dmg

    private void Start() {
        //intializing variables
        maxHealth = setMaxHealth;
        speed = setSpeed;

        currentHealth = maxHealth;
    }

    private void Update() {
        if (currentHealth < maxHealth && !(IsInvoking())) {
            Invoke("Regen", 1.0f);
        }
    }

    public void Instantiate() {
        Start();
    }

    public void UpdateHealth(float mod) {
        currentHealth += mod;
        if (Logger != null)
            Logger.GetComponent<FeedInvoker>().DamageTakenIndicator((int)mod);
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        } else if (currentHealth <= 0.0f) {
            currentHealth = 0.0f;
            Respawn();
        }
    }

    private void Regen() {
        currentHealth += regenPerSecond;
    }

    private void Respawn() {
        SetSpeed(0.0f);
        Debug.Log("YOU DIED");
        GetComponent<Rigidbody2D>().position = new Vector2(0.0f, 0.0f);
        Invoke("RespawnComplete", 3.0f);
    }

    private void RespawnComplete() {
        SetSpeed(setSpeed);
        currentHealth = maxHealth;
    }

    void OnGUI() {
        GUI.Label(new Rect(16, 14, 1500, 100), "Health: " + currentHealth.ToString());
    }

    ///////////////////////////////////////////////
    // PlayerInterface Function Implementations: //
    ///////////////////////////////////////////////

    public Vector2 GetPosition() {
        return GetComponent<Rigidbody2D>().position;
    }

    public void SetPosition(Vector2 position) {
        GetComponent<Rigidbody2D>().position = position;
    }

    public float GetSpeed() {
        return speed;
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public void SetMaxHealth(float newMaxHealth) {
        maxHealth = newMaxHealth;
    }

    public float GetCurrentHealth() {
        return currentHealth;
    }

    public void SetCurrentHealth(float newCurrentHealth) {
        currentHealth = newCurrentHealth;
    }

    public float GetSwingSpeed() {
        return swingSpeed;
    }

    public void SetSwingSpeed(float newSwingSpeed) {
        swingSpeed = newSwingSpeed;
    }

    public float GetRPS() {
        return regenPerSecond;
    }

    public void SetRPS(float newRPS) {
        regenPerSecond = newRPS;
    }

    public float GetDMG() {
        return damage;
    }

    public void SetDMG(float newDMG) {
        damage = newDMG;
    }

}
