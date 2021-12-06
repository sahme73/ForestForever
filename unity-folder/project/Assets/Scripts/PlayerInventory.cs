using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

/*
Interface: 
CraftingInventory():        Create an empty inventory with crafts
CraftingInventory(string):  Create a hased inventory initialized with crafts

ToString():                 Print to Console
AddItem(string):            Add 1 item of name string
AddItem(string, int):       Add int item of name string
GetCount(string):           Get number of items named string
GetItems():                 Get the dictionary of item and counts

ToSaveHash():               Get a prime#-based string hash of the items
CraftingInventory(string):  Create an inventory based on a save hash
                            if invalid, throw an error

AddCrafting(string, dict)   Add recipe dict for product string
Craft(string)               Craft one product string if possible; 
                            returns T/F if successfully crafted
GetCraftings():             Get the nested dictionary of crafts and recipes
*/
public class PlayerInventory : MonoBehaviour, InventoryInterface
{
  //private fields
  private Dictionary<string, int> items = new Dictionary<string, int>();
  private Dictionary<
    string, 
    Dictionary<string, int>
  > 
  craftings = new Dictionary<
    string, 
    Dictionary<string, int>
  > ();
  
  public void CraftingInventory(){
    AddDefaultCrafts();
  }
  
  //public storage interface
  public override string ToString(){
    string itemsPrint = "In Inventory: \n" + DictToString(items);
    
    string craftingsPrint = "Crafting Recipes: \n";
    foreach(var pair in craftings) {
      craftingsPrint += "  " + pair.Key + ":\n" +
      DictToString(pair.Value);
    }
    if(craftingsPrint == "Crafting Recipes: \n"){
      craftingsPrint += " " + "EMPTY.";
    }
    
    return "--------------\n" + 
    itemsPrint + "\n" + craftingsPrint +
    "\n--------------\n";
  }
  
  public void AddItem(string item, int increment){
    if(!items.ContainsKey(item)){
      items[item] = 0;
    }
    items[item] += increment;
  }

  public void AddItem(string item){
    AddItem(item, 1);
  }

  public void RemoveItem(string item) {
    if (!items.ContainsKey(item)) {
    } else if (items[item] > 0) {
      items[item] -= 1;
    }
  }

  public bool HasItem(string item) {
    if (!items.ContainsKey(item)) return false;
    if (items[item] > 0) return true;
    return false;
  }

  public int GetCount(string item){
    if(!items.ContainsKey(item)){
      return 0;
    }
    return items[item];
  }

  public Dictionary<string, int> GetItems() {
    return items;
  }

  //public saving interface
  public string ToSaveHash(){
    string toPrint = "";
    int hashValue = 0;
    foreach(var pair in items) {
      toPrint += pair.Key + ":" + Convert.ToString(pair.Value) + ":";
      hashValue = HashStringFunction(hashValue, pair.Key);
      hashValue = HashFunction(hashValue, pair.Value);
    }
    return Convert.ToString(hashValue) + ":" + toPrint;
  }
  
  public void CraftingInventory(string hash){
    AddDefaultCrafts();
    
    int next = hash.IndexOf(":");
    int trueHashValue = Int32.Parse(hash.Substring(0, next));
    hash = hash.Substring(next + 1);
    int attempedHashValue = 0;
    
    while(hash.IndexOf(":") != -1){
      next = hash.IndexOf(":");
      string item = hash.Substring(0, next);
      hash = hash.Substring(next + 1);
      attempedHashValue = HashStringFunction(attempedHashValue, item);
      
      next = hash.IndexOf(":");
      int number = Int32.Parse(hash.Substring(0, next));
      hash = hash.Substring(next + 1);
      attempedHashValue = HashFunction(attempedHashValue, number);
      
      AddItem(item, number);
    }
    
    if(attempedHashValue != trueHashValue){
      throw new ArgumentException("Invalid hash! ...Are you trying to cheat?...");
    }
  }

  //public crafting interface
  public void AddCrafting(string product, Dictionary<string, int> requirements){
    craftings[product] = requirements;
  }
  
  public bool Craft(string product){
    if(!craftings.ContainsKey(product) || !Satisfied(craftings[product])){
      return false;
    }
    Dock(craftings[product]);
    AddItem(product);
    return true;
  }    
  public Dictionary<string, Dictionary<string, int>> GetCraftings(){
    return craftings;
  }

  //private helper functions
  private static string DictToString(Dictionary<string, int> dict){
    string toPrint = "";
    foreach(var pair in dict) {
      toPrint += "    " + 
      pair.Key + ": " + Convert.ToString(pair.Value) + 
      "\n";
    }
    if(toPrint == ""){
      toPrint += " " + "EMPTY. \n";
    }
    
    return toPrint;      
  }    

  private bool Satisfied(Dictionary<string, int> requirements){
    foreach(var pair in requirements) {
      if(GetCount(pair.Key) < pair.Value){
        return false;
      }
    }
    return true;
  }
  
  private void Dock(Dictionary<string, int> requirements){
    foreach(var pair in requirements) {
      AddItem(pair.Key, -pair.Value);
    }
  }
  
  private void AddDefaultCrafts(){
    
    //tinder is a basic conversion for campfires
    AddCrafting(
    "Tinder", 
    new Dictionary<string, int>(){
      {"Wood", 1}
    });
    
    //campfires allow you to make different wood types
    AddCrafting(
    "Small Campfire", 
    new Dictionary<string, int>(){
      {"Tinder", 10},
      {"Wood", 10} 
    });
    AddCrafting(
    "Medium Campfire", 
    new Dictionary<string, int>(){
      {"Tinder", 10},
      {"Wood", 100} 
    });
    AddCrafting(
    "Large Campfire", 
    new Dictionary<string, int>(){
      {"Tinder", 10},
      {"Wood", 500} 
    });
    
    //better wood allows for better crafts
    AddCrafting(
    "Smoke-Treated Wood", 
    new Dictionary<string, int>(){
      {"Small Campfire", 1},
      {"Wood", 10} 
    });
    AddCrafting(
    "Tempered Wood", 
    new Dictionary<string, int>(){
      {"Medium Campfire", 1},
      {"Wood", 50} 
    });
    AddCrafting(
    "Fire-Hardened Wood", 
    new Dictionary<string, int>(){
      {"Large Campfire", 1},
      {"Wood", 100}
    });
    
    //better axes to cut faster
    //multiple purchases?
    AddCrafting(
    "Axe Reinforcement", 
    new Dictionary<string, int>(){
      {"Smoke-Treated Wood", 1}
    });
    // base upgrade
    AddCrafting(
    "Powerful Axe", 
    new Dictionary<string, int>(){
      {"Tempered Wood", 1},
      {"Wood", 10}
    });
    AddCrafting(
    "Razor-edged Axe", 
    new Dictionary<string, int>(){
      {"Fire-Hardened Wood", 1},
      {"Wood", 50}
    });
    
    //utility
    //running faster
    AddCrafting(
    "Boots Reinforcement", 
    new Dictionary<string, int>(){
      {"Smoke-Treated Wood", 5},
      {"Wood", 10}
    }); 
  }

  private int HashStringFunction(int hash, string input){
    int newHash = hash;
    foreach(var ch in input){
      newHash = HashFunction(newHash, (int)(ch));
    }      
    return newHash;
  }
  
  private int HashFunction(int hash, int input){
    return (hash * 7877 + input * input) % 5303;
  }

  public void EmptyInventory() {
    items = new Dictionary<string, int>();
  }
}
