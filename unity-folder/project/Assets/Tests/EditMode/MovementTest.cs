using NUnit.Framework;
using UnityEngine;
using System;

/*The following tests essentially assure that the Move() function properly calculates the 
  position change according to the speed and direction (velocity vector)               */

public class MovementTest
{

  [Test]
  public void UpwardChange() {
    // dummy object instantiation:
    GameObject temp = new GameObject("temp");
    temp.AddComponent<PlayerMovement>();
    temp.AddComponent<PlayerStats>();
    temp.AddComponent<Rigidbody2D>();
    PlayerMovement pm = temp.GetComponent<PlayerMovement>();
    pm.player = temp.GetComponent<Rigidbody2D>();

    // initializing dummy PlayerMovement (pm) object:
    pm.player.position = Vector2.zero;
    pm.SetMovement(0.0f, 1.0f); // upward input
    pm.GetComponent<PlayerStats>().SetSpeed(10.0f);
    pm.Move();
    
    // calculations:
    Vector2 final_vel = pm.MoveCalculation();
    // physics formula: [x_0,y_0]+v*t
    // expecting: [0,0]+[0,1]*10*0.02 = [0,0.2]
    Vector2 expected_vel = new Vector2(0.0f, 0.2f);

    // rounding floating point values:
    decimal final_vel_x = Math.Round((decimal)final_vel.x, 2);
    decimal final_vel_y = Math.Round((decimal)final_vel.y, 2);

    // using assert class to verify final = expected:
    Assert.AreEqual(final_vel_x, expected_vel.x);
    Assert.AreEqual(final_vel_y, expected_vel.y);

    // destroy dummy object:
    GameObject.DestroyImmediate(temp);
  }

  [Test]
  public void DownwardChange() {
    // dummy object instantiation:
    GameObject temp = new GameObject("temp");
    temp.AddComponent<PlayerMovement>();
    temp.AddComponent<PlayerStats>();
    temp.AddComponent<Rigidbody2D>();
    PlayerMovement pm = temp.GetComponent<PlayerMovement>();
    pm.player = temp.GetComponent<Rigidbody2D>();

    // initializing dummy PlayerMovement (pm) object:
    pm.player.position = Vector2.zero;
    pm.SetMovement(0.0f, -1.0f); // upward input
    pm.GetComponent<PlayerStats>().SetSpeed(10.0f);
    pm.Move();
    
    // calculations:
    Vector2 final_vel = pm.MoveCalculation();
    // physics formula: [x_0,y_0]+v*t
    // expecting: [0,0]+[0,-1]*10*0.02 = [0,-0.2]
    Vector2 expected_vel = new Vector2(0.0f, -0.2f);

    // rounding floating point values:
    decimal final_vel_x = Math.Round((decimal)final_vel.x, 2);
    decimal final_vel_y = Math.Round((decimal)final_vel.y, 2);

    // using assert class to verify final = expected:
    Assert.AreEqual(final_vel_x, expected_vel.x);
    Assert.AreEqual(final_vel_y, expected_vel.y);

    // destroy dummy object:
    GameObject.DestroyImmediate(temp);
  }

  [Test]
  public void RightwardChange() {
    // dummy object instantiation:
    GameObject temp = new GameObject("temp");
    temp.AddComponent<PlayerMovement>();
    temp.AddComponent<PlayerStats>();
    temp.AddComponent<Rigidbody2D>();
    PlayerMovement pm = temp.GetComponent<PlayerMovement>();
    pm.player = temp.GetComponent<Rigidbody2D>();

    // initializing dummy PlayerMovement (pm) object:
    pm.player.position = Vector2.zero;
    pm.SetMovement(1.0f, 0.0f); // upward input
    pm.GetComponent<PlayerStats>().SetSpeed(10.0f);
    pm.Move();
    
    // calculations:
    Vector2 final_vel = pm.MoveCalculation();
    // physics formula: [x_0,y_0]+v*t
    // expecting: [0,0]+[1,0]*10*0.02 = [0.2,0.0]
    Vector2 expected_vel = new Vector2(0.2f, 0.0f);

    // rounding floating point values:
    decimal final_vel_x = Math.Round((decimal)final_vel.x, 2);
    decimal final_vel_y = Math.Round((decimal)final_vel.y, 2);

    // using assert class to verify final = expected:
    Assert.AreEqual(final_vel_x, expected_vel.x);
    Assert.AreEqual(final_vel_y, expected_vel.y);

    // destroy dummy object:
    GameObject.DestroyImmediate(temp);
  }

  [Test]
  public void LeftwardChange() {
    // dummy object instantiation:
    GameObject temp = new GameObject("temp");
    temp.AddComponent<PlayerMovement>();
    temp.AddComponent<PlayerStats>();
    temp.AddComponent<Rigidbody2D>();
    PlayerMovement pm = temp.GetComponent<PlayerMovement>();
    pm.player = temp.GetComponent<Rigidbody2D>();

    // initializing dummy PlayerMovement (pm) object:
    pm.player.position = Vector2.zero;
    pm.SetMovement(-1.0f, 0.0f); // upward input
    pm.GetComponent<PlayerStats>().SetSpeed(10.0f);
    pm.Move();
    
    // calculations:
    Vector2 final_vel = pm.MoveCalculation();
    // physics formula: [x_0,y_0]+v*t
    // expecting: [0,0]+[-1,0]*10*0.02 = [-0.2,0.0]
    Vector2 expected_vel = new Vector2(-0.2f, 0.0f);

    // rounding floating point values:
    decimal final_vel_x = Math.Round((decimal)final_vel.x, 2);
    decimal final_vel_y = Math.Round((decimal)final_vel.y, 2);

    // using assert class to verify final = expected:
    Assert.AreEqual(final_vel_x, expected_vel.x);
    Assert.AreEqual(final_vel_y, expected_vel.y);

    // destroy dummy object:
    GameObject.DestroyImmediate(temp);
  }
}
