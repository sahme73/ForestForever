using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  /*This script is a basic implementation of movement in a 2D space.*/

public class PlayerMovement : MonoBehaviour {
  // Public Variables:
  public Rigidbody2D player; // rigidbody for the current player object
  public bool isTesting = false; // for unit tests
  public Animator animator;

  // Private Variables:
  private Vector2 movement; // the 2d vector of movement velocity (includes direction)

  // Start is called before the first frame update
  private void Start() {
    player = GetComponent<Rigidbody2D>(); // initializes the player's rigidbody to the current object
  }

  // Update is called once per frame | Input handler
  private void Update() {
    // Updated Approach //
    if (!isTesting) {
      movement.x = Input.GetAxisRaw("Horizontal");
      movement.y = Input.GetAxisRaw("Vertical");

      animator.SetFloat("Speed", movement.x);
      animator.SetFloat("VerticalSpeed", Mathf.Abs(movement.y));
    }
  }

  // Update is called 50 times per second | Physics handler
  private void FixedUpdate() {
    Move();
  }

  public void Move() {
    player.MovePosition(MoveCalculation());
  }

  public Vector2 MoveCalculation() {
    return (player.position + movement * player.GetComponent<PlayerStats>().GetSpeed() * Time.fixedDeltaTime);
  }

  public void SetMovement(float x, float y) {
    movement.x = x;
    movement.y = y;
  }
}