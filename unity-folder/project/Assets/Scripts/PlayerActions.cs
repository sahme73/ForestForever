using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TreeStats;

public class PlayerActions : MonoBehaviour
{
    public Collider2D player;
    public GameObject Logger;
    public GameObject SwingHitbox;
    public GameObject InventoryMenu;

    private bool showInventory = false;

    private bool isSwinging = false;

    //public PlayerInventory inventory;

    private void Update() {
        if (Input.GetKeyDown("e")) showInventory = !showInventory; // toggle inventory shown/hidden by clicking 'e'

        if (Input.GetKeyDown("space") && !IsInvoking()) { 
          isSwinging = true; // swinging weapon in hand
          SwingHitbox.SetActive(true);
        }

        // plant tree
        if (Input.GetKey("p") && GetComponent<PlayerInventory>().HasItem("Seed") && !IsInvoking("PlantCooldown")) {
          GetComponent<PlayerInventory>().RemoveItem("Seed");
          
          GameObject treePrefab = GameObject.FindGameObjectWithTag("TreeOrigin");
          GameObject clone = Instantiate(treePrefab, transform.position, Quaternion.identity) as GameObject;
          clone.tag = "TreeParent";
          TreeInterface treeI = clone.GetComponentInChildren<TreeInterface>();

          treeI.SetPosition(transform.position);
          treeI.SetHealth(0.1f);
          treeI.SetAge(Age.Seedling);
        
          Invoke("PlantCooldown", 1.0f);
        }

        if (isSwinging) Invoke("SwingCooldown", GetComponent<PlayerStats>().swingSpeed);

        if (showInventory && !(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GamePause>().PauseStatus())) {
          PlayerInventory inventory = player.GetComponent<PlayerInventory>();
          Dictionary<string, int> items = inventory.GetItems();
          string inventoryString = "Inventory:\n";
          foreach (var item in items) {
            inventoryString += (item.Key + ": \t " + item.Value.ToString() + "\n");
          }
          InventoryMenu.GetComponentInChildren<Text>().text = inventoryString;
          InventoryMenu.SetActive(true);
        } else {
          InventoryMenu.SetActive(false);
        }
    }

    private void SwingCooldown() {
      isSwinging = false;
      SwingHitbox.SetActive(false);
    }

    private void PlantCooldown() {}

    private void OnTriggerStay2D(Collider2D other) {
      if (isSwinging) {
        if (other.gameObject.tag == "Tree") {
          // tree takes damage and indicates how much wood the player should receive
          int woodGained = other.gameObject.GetComponent<TreeScript>().TakeDamage(player.GetComponent<PlayerStats>().damage);
          Logger.GetComponentInChildren<FeedInvoker>().WoodCutIndicator(woodGained);
          GetComponent<PlayerInventory>().AddItem("Wood", woodGained);
          Debug.Log("Hit " + other.name + "!");
        }
        if (other.gameObject.tag == "Enemy") {
          other.gameObject.GetComponent<Enemy>().TakeDamage(player.GetComponent<PlayerStats>().damage);
          Logger.GetComponent<FeedInvoker>().DamageDoneIndicator((int)GetComponent<PlayerStats>().damage);
          Debug.Log("Hit " + other.name + "!");
        }
        isSwinging = false;
      }
    }
}
