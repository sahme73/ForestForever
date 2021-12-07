using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class IsMoving
{
  private GameObject testObject;

  [SetUp]
  public void Setup() {
    // Instantiate test object at origin without gravity.
    testObject = GameObject.Instantiate(new GameObject());
    testObject.AddComponent<PlayerMovement>();
    testObject.AddComponent<PlayerStats>();
    testObject.AddComponent<Rigidbody2D>();
    testObject.GetComponent<Rigidbody2D>().transform.position = Vector3.zero;
    testObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    testObject.GetComponent<PlayerMovement>().isTesting = true;
  }
  
  [UnityTest]
  public IEnumerator MoveUp() {
    
    // Movement direction set.
    testObject.GetComponent<PlayerMovement>().SetMovement(0.0f, 1.0f);

    // Use yield to progress time.
    yield return new WaitForSeconds(5.0f);

    int xPositionRounded = Mathf.RoundToInt(testObject.GetComponent<PlayerMovement>().player.position.x);
    int yPositionRounded = Mathf.RoundToInt(testObject.GetComponent<PlayerMovement>().player.position.y);

    // Assert that the object's position moved properly.
    Assert.AreEqual(xPositionRounded, 0);
    Assert.AreEqual(yPositionRounded, 50);

  }

  [UnityTest]
  public IEnumerator MoveDown() {

    // Movement direction set.
    testObject.GetComponent<PlayerMovement>().SetMovement(0.0f, -1.0f);

    // Use yield to progress time.
    yield return new WaitForSeconds(5.0f);

    int xPositionRounded = Mathf.RoundToInt(testObject.GetComponent<PlayerMovement>().player.position.x);
    int yPositionRounded = Mathf.RoundToInt(testObject.GetComponent<PlayerMovement>().player.position.y);

    // Assert that the object's position moved properly.
    Assert.AreEqual(xPositionRounded, 0);
    Assert.AreEqual(yPositionRounded, -50);

  }

  [UnityTest]
  public IEnumerator MoveRight() {
    
    // Movement direction set.
    testObject.GetComponent<PlayerMovement>().SetMovement(1.0f, 0.0f);

    // Use yield to progress time.
    yield return new WaitForSeconds(5.0f);

    int xPositionRounded = Mathf.RoundToInt(testObject.GetComponent<PlayerMovement>().player.position.x);
    int yPositionRounded = Mathf.RoundToInt(testObject.GetComponent<PlayerMovement>().player.position.y);

    // Assert that the object's position moved properly.
    Assert.AreEqual(xPositionRounded, 50);
    Assert.AreEqual(yPositionRounded, 0);

  }

  [UnityTest]
  public IEnumerator MoveLeft() {
    
    // Movement direction set.
    testObject.GetComponent<PlayerMovement>().SetMovement(-1.0f, 0.0f);

    // Use yield to progress time.
    yield return new WaitForSeconds(5.0f);

    int xPositionRounded = Mathf.RoundToInt(testObject.GetComponent<PlayerMovement>().player.position.x);
    int yPositionRounded = Mathf.RoundToInt(testObject.GetComponent<PlayerMovement>().player.position.y);

    // Assert that the object's position moved properly.
    Assert.AreEqual(xPositionRounded, -50);
    Assert.AreEqual(yPositionRounded, 0);

  }

  [TearDown]
  public void TearDown() {
    GameObject.Destroy(testObject);
  }

}
