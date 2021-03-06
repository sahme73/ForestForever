using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TreeStats {
  public class TreeScript : MonoBehaviour, TreeInterface {
    
    public Text hpBar;
    public float currentHP;
    public Age currentAge;
    public float growthSpeed = 0.2f;
    public SpriteRenderer grown;
    public SpriteRenderer baby;
    private Vector2 originalScale;
    private Vector2 currScale;
    private bool flag;

    private void Start() {
      currScale = originalScale = gameObject.GetComponent<RectTransform>().localScale;
      if (currentAge == Age.Grown) flag = true;
      else flag = false;
    }

    private void Update() {
      if (currentHP <= 0.0f) {
        Debug.Log(name + " died!");
        GameObject.Destroy(gameObject);
        GameObject.Destroy(hpBar);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerActions>().Logger.GetComponentInChildren<FeedInvoker>().ItemAddIndicator("Seed", 1);
      }
      if (currentAge == Age.Seedling && !flag) {
        
          currScale.x /= 2.0f;
          currScale.y /= 2.0f;
          gameObject.GetComponent<RectTransform>().localScale = currScale;
        
        gameObject.GetComponent<SpriteRenderer>().sprite = baby.sprite;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        flag = true;
      }
      if (currentAge == Age.Seedling && !IsInvoking()) {
        Invoke("Grow", growthSpeed);
      }
      hpBar.text = currentHP.ToString();
    }

    private void Grow() {
      if (currentHP == 0.1f) {
        currentHP = 1.0f;
      } else if (currentHP >= 100.0f) {
        currentHP = 100.0f;
        currentAge = Age.Grown;
        gameObject.GetComponent<RectTransform>().localScale = originalScale;
        currScale = originalScale;
        gameObject.GetComponent<SpriteRenderer>().sprite = grown.sprite;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
      } else {
        currentHP += 1.0f;
      }
    }

    public int TakeDamage(float dmg) {
      if (currentAge == Age.Grown) {
        int output = 0;
        if ((currentHP - dmg) > 0) {
          output = (int)dmg;
        } else {
          output = (int)currentHP;
        }
        currentHP -= dmg;
        return output;
      }
      return 0;
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