using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedInvoker : MonoBehaviour {
    private GameObject Logger;

  private void Awake() {
    Logger = gameObject;
  }

  public void WoodCutIndicator(int woodGained) {
      Logger.SetActive(true);
      Logger.GetComponent<Text>().text = "+" + woodGained + " Wood!";
      Invoke("LoggerPause", 1.0f);
    }

    public void DamageDoneIndicator(int dmgDone) {
      Logger.SetActive(true);
      Logger.GetComponent<Text>().text = "+" + dmgDone + " Damage Done!";
      Invoke("LoggerPause", 1.0f);
    }

    public void DamageTakenIndicator(int dmgTaken) {
      Logger.SetActive(true);
      Logger.GetComponent<Text>().text = dmgTaken + " Damage Taken!";
      Invoke("LoggerPause", 1.0f);
    }

    public void SaveIndicator() {
      Logger.SetActive(true);
      Logger.GetComponent<Text>().text = "Game Saved!";
      Invoke("LoggerPause", 1.0f);
    }

    public void LoadIndicator() {
      Logger.SetActive(true);
      Logger.GetComponent<Text>().text = "Game Loaded!";
      Invoke("LoggerPause", 1.0f);
    }

    public void BrokenSaveIndicator() {
      Logger.SetActive(true);
      Logger.GetComponent<Text>().text = "No Save Found!";
      Invoke("LoggerPause", 1.0f);
    }

    private void LoggerPause() {
        Logger.SetActive(false);
    }
}
