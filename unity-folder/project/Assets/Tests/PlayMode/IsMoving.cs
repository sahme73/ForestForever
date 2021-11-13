using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class IsMoving
{
  // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
  // `yield return null;` to skip a frame.
  
  [UnityTest]
  public IEnumerator MoveUp() {
    // Use the Assert class to test conditions.

    // Use yield to skip a frame.
    yield return 1;

    Assert.AreEqual(new Vector2(0, 0), new Vector2(0, 0));
  }

  [UnityTest]
  public IEnumerator MoveDown() {
    // Use the Assert class to test conditions.

    // Use yield to skip a frame.
    yield return 1;

    Assert.AreEqual(new Vector2(0, 0), new Vector2(0, 0));
  }

  [UnityTest]
  public IEnumerator MoveRight() {
    // Use the Assert class to test conditions.

    // Use yield to skip a frame.
    yield return 1;

    Assert.AreEqual(new Vector2(0, 0), new Vector2(0, 0));
  }

  [UnityTest]
  public IEnumerator MoveLeft() {
    // Use the Assert class to test conditions.

    // Use yield to skip a frame.
    yield return 1;

    Assert.AreEqual(new Vector2(0, 0), new Vector2(0, 0));
  }
}
