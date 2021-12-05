using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyInterface {
  public Vector2 GetPosition();

  public void SetPosition(Vector2 newPosition);
  public Vector3 GetScale();
  public void SetScale(Vector3 newScale);
  public Color GetColor();
  public void SetColor(Color newColor);
  public float GetSpeed();
  public void SetSpeed(float newSpeed);
  public float GetHealth();
  public void SetHealth(float newHealth);
  public float GetBasicDMG();
  public void SetBasicDMG(float newBasicDMG);
  public float GetBasicSpeed();
  public void SetBasicSpeed(float newBasicSpeed);
}