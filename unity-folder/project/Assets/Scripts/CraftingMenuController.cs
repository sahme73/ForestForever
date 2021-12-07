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
    public GameObject menu;
    public Text recipe;
    private string lastSelected; 
    // Start is called before the first frame update
    void Start()
    {
        lastSelected = "null";
        menu.GetComponent<Image>().color = new Color(41, 95, 145, 0f);
        btn.GetComponentInChildren<Text>().text = "Craft";
        btn.GetComponent<Button>().gameObject.SetActive(false);
        recipe.GetComponent<Text>().gameObject.SetActive(false);
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
        lastSelected = dropdown.options[drop.value].text;
        if (drop.value > 0) {
            btn.GetComponent<Button>().gameObject.SetActive(true);
            recipe.GetComponent<Text>().gameObject.SetActive(true);
            menu.GetComponent<Image>().color = new Color(41, 95, 145, 0.8f);
            Dictionary<string, Dictionary<string, int>> craftables = inventory.GetCraftings();
            Dictionary<string, int> reqs = craftables[lastSelected];
            string recipeText = "Recipe:\n";
            foreach (var req in reqs) {
                recipeText += req.Key;
                recipeText += "   " + req.Value.ToString();
                recipeText += "\n";
            }
            recipe.text = recipeText;
        }
        //Debug.Log(drop.value);
        //PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        btn.GetComponentInChildren<Text>().text = "Craft " + lastSelected;
        if (!inventory.Satisfied(lastSelected, 1)) btn.GetComponentInChildren<Text>().color = Color.red;
        else btn.GetComponentInChildren<Text>().color = Color.black;
    }

    void craftClicked() {
        bool success = inventory.Craft(lastSelected);
        FeedInvoker feed = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerActions>().Logger.GetComponentInChildren<FeedInvoker>();
        if (success) 
            feed.ItemCraftIndicator(lastSelected, 1);
        else
            feed.CannotCraftIndicator(lastSelected);
        btn.GetComponent<Button>().gameObject.SetActive(false);
        recipe.GetComponent<Text>().gameObject.SetActive(false);
        menu.GetComponent<Image>().color = new Color(41, 95, 145, 0f);
    }
}
