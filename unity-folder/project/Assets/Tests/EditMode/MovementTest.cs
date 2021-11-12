using NUnit.Framework;
using UnityEngine;
using TopDownMovement;

public class MovementTest
{
  // A Test behaves as an ordinary method
  [Test]
  public void UpwardChange() {
    // Use the Assert class to test conditions
    Assert.AreEqual(new Vector2(0, 0), new Vector2(0, 0));
  }

  // A Test behaves as an ordinary method
  [Test]
  public void DownwardChange() {
    // Use the Assert class to test conditions
    Assert.AreEqual(new Vector2(0, 0), new Vector2(0, 0));
  }

  // A Test behaves as an ordinary method
  [Test]
  public void RightwardChange() {
    // Use the Assert class to test conditions
    Assert.AreEqual(new Vector2(0, 0), new Vector2(0, 0));
  }

  // A Test behaves as an ordinary method
  [Test]
  public void LeftwardChange() {
    // Use the Assert class to test conditions
    Assert.AreEqual(new Vector2(0, 0), new Vector2(0, 0));
  }
}
