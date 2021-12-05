using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerInterface {
  public Vector2 GetPosition();
  public void SetPosition(Vector2 position);
  public float GetSpeed();
  public void SetSpeed(float newSpeed);
  public float GetMaxHealth();
  public void SetMaxHealth(float newMaxHealth);
  public float GetCurrentHealth();
  public void SetCurrentHealth(float newCurrentHealth);
  public float GetSwingSpeed();
  public void SetSwingSpeed(float newSwingSpeed);
  public float GetRPS();
  public void SetRPS(float newRPS);
  public float GetDMG();
  public void SetDMG(float newDMG);
}
