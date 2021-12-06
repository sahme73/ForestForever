using NUnit.Framework;
using UnityEngine;
using System;

// Test the health of the the player under different situations

public class Health
{
    [Test]
    public void AddBasicItem() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerInventory>();

        GetComponent<PlayerInventory>().AddItem("Wood", 10);
        GetComponent<PlayerInventory>().AddItem("Seed", 100);

        Assert.AreEqual(
            GetComponent<PlayerInventory>().GetCount("Wood"), 
            10);

        Assert.AreEqual(
            GetComponent<PlayerInventory>().GetCount("Seed"), 
            100);

        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }

    [Test]
    public void SucessfulCraft() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerInventory>();

        GetComponent<PlayerInventory>().AddItem("Wood", 10);
        GetComponent<PlayerInventory>().AddItem("Tinder", 10);

        Assert.IsTrue(GetComponent<PlayerInventory>().Craft("Small Campfire"));


        Assert.AreEqual(
            GetComponent<PlayerInventory>().GetCount("Wood"), 
            0);

        Assert.AreEqual(
            GetComponent<PlayerInventory>().GetCount("Tinder"), 
            0);

        Assert.AreEqual(
            GetComponent<PlayerInventory>().GetCount("Small Campfire"), 
            1);

        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }

    [Test]
    public void UnsucessfulCraft() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerInventory>();

        GetComponent<PlayerInventory>().AddItem("Wood", 9);
        GetComponent<PlayerInventory>().AddItem("Tinder", 10);

        Assert.IsTrue(!GetComponent<PlayerInventory>().Craft("Small Campfire"));

        Assert.AreEqual(
            GetComponent<PlayerInventory>().GetCount("Wood"), 
            9);

        Assert.AreEqual(
            GetComponent<PlayerInventory>().GetCount("Tinder"), 
            10);

        Assert.AreEqual(
            GetComponent<PlayerInventory>().GetCount("Small Campfire"), 
            0);

        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }
}
