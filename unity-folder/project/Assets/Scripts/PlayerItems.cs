using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour {
  public GameObject player;
  private PlayerInventory inventory;
  private PlayerStats stats;
  private void Start() {
    inventory = player.GetComponent<PlayerInventory>();
    stats = player.GetComponent<PlayerStats>();
  }

  private void Update() {
    // Boots
    if (inventory.HasItem("Boots Reinforcement")) {
      stats.SetSpeed(stats.GetSpeed() + 5.0f);
    }

    // Axe
    if (inventory.HasItem("Axe Reinforcement")) {
      stats.SetDMG(stats.GetDMG() + 5.0f);
      inventory.RemoveItem("Axe Reinforcement");
    } else if (inventory.HasItem("Powerful Axe")) {
      stats.SetSwingSpeed(0.75f);
      inventory.RemoveItem("Powerful Axe");
    } else if (inventory.HasItem("Razor-edged Axe")) {
      stats.SetSwingSpeed(0.50f);
      inventory.RemoveItem("Razor-edged Axe");
    }
  }

}
