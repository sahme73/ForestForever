using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerLight : MonoBehaviour {
  public GameObject player;
  public GameObject playerLight;
  public Light2D copy;
  private Light2D light2D;

  private void Update() {
    light2D = playerLight.GetComponent<Light2D>();
    PlayerInventory inventory = player.GetComponent<PlayerInventory>();
    if (inventory.HasItem("Large Torch")) {
      light2D.color = copy.color;  
      light2D.intensity = 0.35f;
      light2D.pointLightInnerRadius = 1.5f;
      light2D.pointLightOuterRadius = 5.0f;
    } else if (inventory.HasItem("Medium Torch")) {
      light2D.color = copy.color;  
      light2D.intensity = 0.30f;
      light2D.pointLightInnerRadius = 1.0f;
      light2D.pointLightOuterRadius = 5.0f;
    } else if (inventory.HasItem("Small Torch")) {
      light2D.color = copy.color;
      light2D.intensity = 0.25f;
      light2D.pointLightInnerRadius = 0.5f;
      light2D.pointLightOuterRadius = 3.0f;
    } else {
      light2D.color = Color.white;
      light2D.intensity = 0.1f;
      light2D.pointLightInnerRadius = 0.5f;
      light2D.pointLightOuterRadius = 3.0f;
    }
  }

}
