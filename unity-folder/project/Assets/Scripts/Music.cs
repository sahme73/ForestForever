using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
  public AudioSource musicController;

  public void Toggle() {
    musicController.mute = !musicController.mute;
  }
}
