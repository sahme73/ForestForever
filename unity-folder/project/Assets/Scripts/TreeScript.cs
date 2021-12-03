using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TreeStats {
  public class TreeScript : MonoBehaviour {
    
    public Text hpBar;
    
    public enum Age {Seedling, Young, Grown}

    public float currentHP;
    public Age currentAge;

    public float GetHealth() {
      return currentHP;
    }

    public Age GetAge() {
      return currentAge;
    }

    public void TakeDamage(float dmg) {
      currentHP -= dmg;
    }

    private void Update() {
      if (currentHP <= 0.0f) {
        Debug.Log(name + " died!");
        GameObject.Destroy(gameObject);
        GameObject.Destroy(hpBar);
      }
      hpBar.text = currentHP.ToString();
    }

  }
}