using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Logger;
    private bool isPaused = false;

    private void Start() {
        Menu.SetActive(false);
        Logger.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
                Menu.SetActive(false);
                isPaused = false;
            } else {
                PauseGame();
                Menu.SetActive(true);
                isPaused = true;
            }
        }
    }

    void PauseGame() {
        Time.timeScale = 0;
    }

    void ResumeGame() {
        Time.timeScale = 1;
    }

    public void ResumePress() {
        if (isPaused) {
            ResumeGame();
            Menu.SetActive(false);
            isPaused = false;
        } else {
            PauseGame();
            Menu.SetActive(true);
            isPaused = true;
        }
    }

    public void QuitPress() {
        Application.Quit();
    }

    public bool PauseStatus() {
      return isPaused;
    }
}
