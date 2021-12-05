using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Age {Seedling, Young, Grown}

public interface TreeInterface {
  public Vector2 GetPosition();
  public void SetPosition(Vector2 newPosition);
  public float GetHealth();
  public void SetHealth(float newHealth);
  public Age GetAge();
  public void SetAge(Age newAge);
  public Text GetHPBar();

}
