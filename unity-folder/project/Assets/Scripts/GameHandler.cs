using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
  [SerializeField] private GameObject playerGameObject;
  private PlayerInterface player;

  private void Awake() {
    player = playerGameObject.GetComponent<PlayerInterface>();
    SaveSystem.Initialize();
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.RightControl)) {
      Save();
    }

    if (Input.GetKeyDown(KeyCode.Backspace)) {
      Load();
    }
  }

  private void Save() {
    Debug.Log("Saving Game...");

    SavePlayer();

    Debug.Log("Saved!");
  }

  private void Load() {
    Debug.Log("Loading Game...");

    LoadPlayer();
  }

  private void SavePlayer() {
    Vector3 playerPosition = player.GetPosition();
    float playerSpeed = player.GetSpeed();
    float playerMaxHealth = player.GetMaxHealth();
    float playerCurrentHealth = player.GetCurrentHealth();
    float playerSwingSpeed = player.GetSwingSpeed();
    float playerRPS = player.GetRPS();
    float playerDMG = player.GetDMG();

    SaveObjectPlayer saveObjectPlayer = new SaveObjectPlayer {
      playerPosition = playerPosition,
      playerSpeed = playerSpeed,
      playerMaxHealth = playerMaxHealth,
      playerCurrentHealth = playerCurrentHealth,
      playerSwingSpeed = playerSwingSpeed,
      playerRPS = playerRPS,
      playerDMG = playerDMG
    };

    string playerJson = JsonUtility.ToJson(saveObjectPlayer);
    SaveSystem.Save(playerJson, "save_player");
  }

  private void LoadPlayer() {
    string saveString = SaveSystem.Load("save_player");
    if (saveString != null) {
      SaveObjectPlayer saveObjectPlayer = JsonUtility.FromJson<SaveObjectPlayer>(saveString);

      player.SetPosition(saveObjectPlayer.playerPosition);
      player.SetSpeed(saveObjectPlayer.playerSpeed);
      player.SetMaxHealth(saveObjectPlayer.playerMaxHealth);
      player.SetCurrentHealth(saveObjectPlayer.playerCurrentHealth);
      player.SetSwingSpeed(saveObjectPlayer.playerSwingSpeed);
      player.SetRPS(saveObjectPlayer.playerRPS);
      player.SetDMG(saveObjectPlayer.playerDMG);

      Debug.Log("Save loaded!");
    } else {
      Debug.Log("No Save Exists!");
    }
  }

  private class SaveObjectPlayer {
    public Vector3 playerPosition;
    public float playerSpeed;
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public float playerSwingSpeed;
    public float playerRPS;
    public float playerDMG;
  }
}
