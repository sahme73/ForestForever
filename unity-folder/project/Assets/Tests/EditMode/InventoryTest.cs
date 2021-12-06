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

        temp.GetComponent<PlayerInventory>().AddItem("Wood", 10);
        temp.GetComponent<PlayerInventory>().AddItem("Seed", 100);

        Assert.AreEqual(
            temp.GetComponent<PlayerInventory>().GetCount("Wood"), 
            10);

        Assert.AreEqual(
            temp.GetComponent<PlayerInventory>().GetCount("Seed"), 
            100);

        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }

    [Test]
    public void SucessfulCraft() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerInventory>();

        temp.GetComponent<PlayerInventory>().AddItem("Wood", 10);
        temp.GetComponent<PlayerInventory>().AddItem("Tinder", 10);

        Assert.IsTrue(GetComponent<PlayerInventory>().Craft("Small Campfire"));


        Assert.AreEqual(
            temp.GetComponent<PlayerInventory>().GetCount("Wood"), 
            0);

        Assert.AreEqual(
            temp.GetComponent<PlayerInventory>().GetCount("Tinder"), 
            0);

        Assert.AreEqual(
            temp.GetComponent<PlayerInventory>().GetCount("Small Campfire"), 
            1);

        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }

    [Test]
    public void UnsucessfulCraft() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerInventory>();

        temp.GetComponent<PlayerInventory>().AddItem("Wood", 9);
        temp.GetComponent<PlayerInventory>().AddItem("Tinder", 10);

        Assert.IsTrue(!temp.GetComponent<PlayerInventory>().Craft("Small Campfire"));

        Assert.AreEqual(
            temp.GetComponent<PlayerInventory>().GetCount("Wood"), 
            9);

        Assert.AreEqual(
            temp.GetComponent<PlayerInventory>().GetCount("Tinder"), 
            10);

        Assert.AreEqual(
            temp.GetComponent<PlayerInventory>().GetCount("Small Campfire"), 
            0);

        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }

    [Test]
    public void AddCraft() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerInventory>();

        temp.GetComponent<PlayerInventory>().AddItem("A", 5);
        temp.GetComponent<PlayerInventory>().AddItem("B", 5);

        temp.GetComponent<PlayerInventory>().AddCrafting(
            "C", 
            new Dictionary<string, int>(){
                {"A", 5},
                {"B", 5}
            });

        Assert.IsTrue(temp.GetComponent<PlayerInventory>().Craft("C"));


        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }

    [Test]
    public void SaveHash() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerInventory>();

        temp.GetComponent<PlayerInventory>().AddItem("ABCDE", 5);
        temp.GetComponent<PlayerInventory>().AddItem("BXXXX", 5);

        GameObject temp2 = new GameObject("temp");
        temp2.AddComponent<PlayerInventory>();

        temp2.GetComponent<PlayerInventory>().AddItem("ABCDE", 5);
        temp2.GetComponent<PlayerInventory>().AddItem("BXXXX", 5);

        Assert.AreEqual(
            temp1.GetComponent<PlayerInventory>().ToSaveHash(),
            temp2.GetComponent<PlayerInventory>().ToSaveHash()
        );


        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }
}
