using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Collider2D player;
    public Collider2D tree;

    private bool showInventory = false;

    //public PlayerInventory inventory;

    private void Update() {
        if (player.IsTouching(tree)) {
            if (Input.GetKeyDown("space")) {
                Debug.Log("+1 Wood!");
                player.GetComponent<PlayerInventory>().AddItem("Wood");
            }
        }

        if (Input.GetKeyDown("e")) showInventory = !showInventory; // toggle inventory shown/hidden by clicking 'e'
    }

    void OnGUI() {
        if (showInventory) {
            PlayerInventory inventory = player.GetComponent<PlayerInventory>();
            Dictionary<string, int> items = inventory.GetItems();
            //Debug.Log("Drawing inventory");
            float yVal = (Screen.height / 2) - 150;
            GUI.Box(new Rect((Screen.width / 2) - 35, yVal, 75, 20 + (30*items.Count)), "Inventory");
            foreach(var item in items) {
                yVal += 30;
                Debug.Log("Drawing " + item.Key + " : " + item.Value);
                GUI.Label(new Rect((Screen.width / 2) - (item.Key.Length  * 6), yVal, 100, 20), item.Key + " : " + item.Value.ToString());
            }
        }
    }
}
