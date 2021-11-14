using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DanielInventory{
  
  /*
  Interface: 
  ToString():                 Print to Console
  AddItem(string):            Add 1 item of name string
  AddItem(string, int):       Add int item of name string
  GetCount(string):           Get number of items named string
  AddCrafting(string, dict)   Add recipe dict for product string
  Craft(string)               Craft one product string if possible; 
                              returns T/F if successfully crafted
  */
  
  class Driver{
    static void Main(string[] args){
      // Testing inventory
      Console.WriteLine("Make empty Inventory:");
      CraftingInventory inventory = new CraftingInventory();
      Console.WriteLine(inventory);
    
      Console.WriteLine("Add materials and recipes:");
      inventory.AddItem("Wood");
      inventory.AddItem("Stone", 5);
      inventory.AddCrafting(
      "Axe", 
      new Dictionary<string, int>(){
        {"Wood", 1}, 
        {"Stone", 3}
      });
      inventory.AddCrafting(
      "Campfire", 
      new Dictionary<string, int>(){
        {"Wood", 10}, 
      });
      Console.WriteLine(inventory);

      Console.WriteLine("Craft an axe and attempt a campfire:");
      inventory.Craft("Axe");
      inventory.Craft("Campfire");
      Console.WriteLine(inventory);

      Console.WriteLine("Add more wood:");
      inventory.AddItem("Wood", 10);
      Console.WriteLine(inventory);      
           
      Console.WriteLine("Craft a campfire:");
      inventory.Craft("Campfire");
      Console.WriteLine(inventory);

    }
  }

  class CraftingInventory{
    
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
    
    public int GetCount(string item){
      if(!items.ContainsKey(item)){
        return 0;
      }
      return items[item];
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
  }
}