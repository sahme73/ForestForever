using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TreeStats;

public class PlayerActions : MonoBehaviour
{
    public Collider2D player;
    public GameObject Logger;

    private bool showInventory = false;

    private bool isSwinging = false;

    //public PlayerInventory inventory;

    private void Update() {
        if (Input.GetKeyDown("e")) showInventory = !showInventory; // toggle inventory shown/hidden by clicking 'e'

        if (Input.GetKeyDown("space") && !IsInvoking()) isSwinging = true; // swinging weapon in hand

        if (isSwinging) Invoke("SwingCooldown", GetComponent<PlayerStats>().swingSpeed);
    }

    private void SwingCooldown() {
      isSwinging = false;
    }

    private void OnTriggerStay2D(Collider2D other) {
      if (isSwinging) {
        if (other.gameObject.tag == "Tree") {
          WoodCut();
          other.gameObject.GetComponent<TreeScript>().TakeDamage(player.GetComponent<PlayerStats>().damage);
          Debug.Log("Hit " + other.name + "!");
        }
        if (other.gameObject.tag == "Enemy") {
          other.gameObject.GetComponent<Enemy>().TakeDamage(player.GetComponent<PlayerStats>().damage);
          Debug.Log("Hit " + other.name + "!");
        }
        isSwinging = false;
      }
    }

    public void WoodCut() {
      Logger.SetActive(true);
      Logger.GetComponent<Text>().text = "+1 Wood!";
      player.GetComponent<PlayerInventory>().AddItem("Wood");
      Invoke("LoggerPause", 1.0f);
    }

    private void LoggerPause() {
        Logger.SetActive(false);
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
                //Debug.Log("Drawing " + item.Key + " : " + item.Value);
                GUI.Label(new Rect((Screen.width / 2) - (item.Key.Length  * 6), yVal, 100, 20), item.Key + " : " + item.Value.ToString());
            }
        }
    }
}
