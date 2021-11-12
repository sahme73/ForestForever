using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownMovement {
  /*This script is a basic implementation of movement in a 2D space.*/

public class PlayerMovement : MonoBehaviour {
    public Rigidbody2D player; // rigidbody for the current player object
    public float speed = 10.0f; // the adjustable speed of the player

    private Vector2 movement; // the 2d vector of movement speeds

    // Start is called before the first frame update
    private void Start() {
      player = GetComponent<Rigidbody2D>(); // initializes the player's rigidbody to the current object
    }

    // Update is called once per frame | Input handler
    private void Update() {
      // Updated Approach //
      movement.x = Input.GetAxisRaw("Horizontal");
      movement.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called 50 times per second | Physics handler
    private void FixedUpdate() {
      Move();
    }

    public void Move() {
      player.MovePosition(player.position + movement * speed * Time.fixedDeltaTime);
    }
  }

}