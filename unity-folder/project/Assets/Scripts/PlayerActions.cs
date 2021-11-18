using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Collider2D player;
    public Collider2D tree;

    //public PlayerInventory inventory;

    private void Update() {
        if (player.IsTouching(tree)) {
            if (Input.GetKeyDown("space")) {
                Debug.Log("+1 Wood!");
                player.GetComponent<PlayerInventory>().AddItem("Wood");
            }
        }

        if (Input.GetKeyDown("e")) {
            Debug.Log(player.GetComponent<PlayerInventory>().ToString());
        }
    }
}
