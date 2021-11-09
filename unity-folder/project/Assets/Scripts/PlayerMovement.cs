using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script is a basic implementation of movement in a 2D space.*/

public class PlayerMovement : MonoBehaviour {
  public Rigidbody2D player; // rigidbody for the current player object
  public float speed = 10.0f; //the adjustable speed of the player

  private float horizontal; // movement speed along the x-axis
  private float vertical;   // movement speed along the y-axis

  private Vector2 movement;

  // Start is called before the first frame update
  private void Start() {
    player = GetComponent<Rigidbody2D>(); // initializes the player's rigidbody to the current object
  }

  // Update is called once per frame | Input handler
  private void Update() {
    
    // Updated Approach //
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");

    // Naive Approach //
    /*
    if (Input.GetKey(KeyCode.UpArrow)) {
      vertical = 1.0f;
      movement.y = vertical;
    }
    if (Input.GetKey(KeyCode.DownArrow)) {
      vertical = -1.0f;
      movement.y = vertical;
    }
    if (Input.GetKey(KeyCode.RightArrow)) {
      horizontal = 1.0f;
      movement.x = horizontal;
    }
    if (Input.GetKey(KeyCode.LeftArrow)) {
      horizontal = -1.0f;
      movement.x = horizontal;
    }
    if (Input.GetKey(KeyCode.Space)) {
      vertical = horizontal = 0;
      movement.x = movement.y = 0;
    }
    */
  }

  // Update is called 50 times per second | Physics handler
  private void FixedUpdate() {
    player.MovePosition(player.position + movement * speed * Time.fixedDeltaTime);
  }
}
