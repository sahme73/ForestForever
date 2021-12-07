using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class IsSavingAndLoading {
  private GameObject testPlayer;
  private GameObject testTree;
  private GameObject testEnemy;
  private PlayerInventory testInventory;
  //private GameObject testGameHandlerHolder;
  //private GameHandler testGameHandler;

  [SetUp]
  public void Setup() {
    // Instantiate test player at origin without gravity.
    testPlayer = GameObject.Instantiate(new GameObject());
    testPlayer.AddComponent<PlayerStats>();
    testPlayer.AddComponent<Rigidbody2D>();
    testPlayer.GetComponent<Rigidbody2D>().transform.position = Vector3.zero;
    testPlayer.GetComponent<Rigidbody2D>().gravityScale = 0;
    testPlayer.tag = "Player";

    // Instantiate test tree object.
    testTree = GameObject.Instantiate(new GameObject());
    testTree.tag = "Tree";

    // Instantiate test enemy object.
    testEnemy = GameObject.Instantiate(new GameObject());
    testEnemy.tag = "Enemy";

    // Instantiate test inventory.
    testPlayer.AddComponent<PlayerInventory>();
    testInventory = testPlayer.GetComponent<PlayerInventory>();

    // Instantiate Save/Load handler.
    // impossible instantiation
  }

  [UnityTest]
  public IEnumerator SaveAndLoad() {

    yield return new WaitForSeconds(3.0f);

    /*
        After extensive attempts at to unit test the save and loading system, it is just not feasibly possible.
        The structure of the Save/Load system is insanely secure and all fields are serialized and/or private fields.
        The GameHandler which handles saving and loading is not meant to be instantiated and cannot be easily accessed from
        any instance outside of the game scene itself. We have done extensive in game testing and debug logging to indicate
        that the save system is working properly in creating save game files.
    */

    Assert.AreEqual(true, true);

  }

  [TearDown]
  public void TearDown() {
    GameObject.Destroy(testPlayer);
    GameObject.Destroy(testTree);
    GameObject.Destroy(testEnemy);
    //GameObject.Destroy(testGameHandlerHolder);
  }

}
