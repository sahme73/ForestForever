using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenuController : MonoBehaviour
{
    public Dropdown dropdown;
    public Collider2D player;
    private PlayerInventory inventory;
    public Button btn;
    private string lastSelected; 
    // Start is called before the first frame update
    void Start()
    {
        lastSelected = "null";
        btn.GetComponentInChildren<Text>().text = "Craft";
        btn.GetComponent<Button>().gameObject.SetActive(false);
        btn.onClick.AddListener(craftClicked);
        inventory = player.GetComponent<PlayerInventory>();
        dropdown.ClearOptions();
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    // Update is called once per frame
    void Update()
    {
        dropdown.ClearOptions();
        //PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        inventory.CraftingInventory();
        Dictionary<string, Dictionary<string, int>> craftings = inventory.GetCraftings();
        
        List<string> options = new List<string>();
        options.Add("Recipes"); // dummy for label
        foreach (var item in craftings) {
            options.Add(item.Key);
        }
        dropdown.AddOptions(options);
    }

    void DropdownValueChanged(Dropdown drop) {
        if (drop.value > 0)
            btn.GetComponent<Button>().gameObject.SetActive(true);
        Debug.Log(drop.value);
        //PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        lastSelected = dropdown.options[drop.value].text;
        if (!inventory.Satisfied(lastSelected, 1)) btn.GetComponentInChildren<Text>().color = Color.red;
        else btn.GetComponentInChildren<Text>().color = Color.black;
    }

    void craftClicked() {
        bool success = inventory.Craft(lastSelected);
        FeedInvoker feed = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerActions>().Logger.GetComponentInChildren<FeedInvoker>();
        if (success) 
            feed.ItemAddIndicator(lastSelected, 1);
        else
            feed.CannotCraftIndicator(lastSelected);
        btn.GetComponent<Button>().gameObject.SetActive(false);
    }
}
