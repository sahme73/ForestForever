using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TreeStats {
  public class TreeScript : MonoBehaviour, TreeInterface {
    
    public Text hpBar;
    public float currentHP;
    public Age currentAge;

    private void Update() {
      if (currentHP <= 0.0f) {
        Debug.Log(name + " died!");
        GameObject.Destroy(gameObject);
        GameObject.Destroy(hpBar);
      }
      hpBar.text = currentHP.ToString();
    }

    public void TakeDamage(float dmg) {
      currentHP -= dmg;
    }

    /////////////////////////////////////////////
    // TreeInterface Function Implementations: //
    /////////////////////////////////////////////

    public Vector2 GetPosition() {
      return GetComponent<Rigidbody2D>().position;
    }

    public void SetPosition(Vector2 newPosition) {
      GetComponent<Rigidbody2D>().position = newPosition;
    }

    public float GetHealth() {
      return currentHP;
    }

    public void SetHealth(float newHealth) {
      currentHP = newHealth;
    }

    public Age GetAge() {
      return currentAge;
    }

    public void SetAge(Age newAge) {
      currentAge = newAge;
    }

    public Text GetHPBar() {
      return hpBar;
    }

  }
}