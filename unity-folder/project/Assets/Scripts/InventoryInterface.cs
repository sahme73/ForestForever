using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryInterface {
  public Dictionary<string, int> GetItems();
  public void AddItem(string item, int increment);
  public void EmptyInventory();
  public string ToSaveHash();
  public Dictionary<string, int> RestoreInventory(string hash);
}
