using NUnit.Framework;
using UnityEngine;
using System;

// Test the health of the the player under different situations

public class Health
{
    
    [Test]

    public void BasicAttack() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerStats>();
        float attack_dmg = -10;

        PlayerStats ps = temp.GetComponent<PlayerStats>();

        ps.UpdateHealth(attack_dmg);
        float final_health = ps.GetCurrentHealth();
        float expected_health = 90;
        Assert.AreEqual(final_health, expected_health);

        ps.UpdateHealth(attack_dmg);
        float final_health = ps.GetCurrentHealth();
        float expected_health = 80;
        Assert.AreEqual(final_health, expected_health);

        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }

    [Test] 
    public void OverMaxHealth() {
        GameObject temp = new GameObject("temp");
        temp.AddComponent<PlayerStats>();

        PlayerStats ps = temp.GetComponent<PlayerStats>();

        ps.UpdateHealth(15);
        float final_health = ps.GetCurrentHealth();
        float expected_health = 100;
        Assert.AreEqual(final_health, expected_health);

        ps.UpdateHealth(15);
        final_health = ps.GetCurrentHealth();
        expected_health = 100;
        Assert.AreEqual(final_health, expected_health);

        // destroy dummy object:
        GameObject.DestroyImmediate(temp);
    }
}
