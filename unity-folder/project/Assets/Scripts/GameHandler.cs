using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
  [SerializeField] private GameObject playerGameObject;
  private PlayerInterface player;
  private InventoryInterface inventory;

  [SerializeField] private GameObject[] treeGameObjects;
  [SerializeField] private GameObject[] enemyGameObjects;

  public void SavePress() {
    Save();
    GameObject.FindGameObjectWithTag("OtherControls").GetComponent<GamePause>().ResumePress();
  }

  public void LoadPress() {
    Load();
    GameObject.FindGameObjectWithTag("OtherControls").GetComponent<GamePause>().ResumePress();
  }

  private void Awake() {
    // player initialization
    player = playerGameObject.GetComponent<PlayerInterface>();
    inventory = playerGameObject.GetComponent<InventoryInterface>();

    // trees initialization
    treeGameObjects = GameObject.FindGameObjectsWithTag("Tree");

    // enemies initialization
    enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");

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

    // trees re-initialization
    treeGameObjects = GameObject.FindGameObjectsWithTag("Tree");

    // enemies re-initialization
    enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");

    SavePlayer();
    SaveTrees();
    SaveInventory();
    SaveEnemies();

    Debug.Log("Saved!");
    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().Logger.GetComponent<FeedInvoker>().SaveIndicator();
  }

  private void Load() {
    Debug.Log("Loading Game...");

    bool isLoaded = LoadPlayer();

    if (!isLoaded) {
      GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().Logger.GetComponent<FeedInvoker>().BrokenSaveIndicator();
      return;
    }

    LoadTrees();
    LoadInventory();
    LoadEnemies();

    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().Logger.GetComponent<FeedInvoker>().LoadIndicator();
  }

  private void SavePlayer() {
    Vector2 playerPosition = player.GetPosition();
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

  private void SaveTrees() {
    string treesJson = "";

    foreach (GameObject tree in treeGameObjects) {
      TreeInterface treeI = tree.GetComponent<TreeInterface>();
      
      Vector2 treePosition = treeI.GetPosition();
      float treeHealth = treeI.GetHealth();
      Age treeAge = treeI.GetAge();
      Text treeHPBAR = treeI.GetHPBar();

      SaveObjectTree saveObjectTree = new SaveObjectTree {
        treePosition = treePosition,
        treeHealth = treeHealth,
        treeAge = treeAge
      };
      
      treesJson += "TREE" + JsonUtility.ToJson(saveObjectTree);
    }

    SaveSystem.Save(treesJson, "save_trees");
  }

  private void SaveInventory() {
    var keys = new List<string>(inventory.GetItems().Keys);
    var vals = new List<int>(inventory.GetItems().Values);

    SaveObjectInventory saveObjectInventory = new SaveObjectInventory {
      keys = keys,
      vals = vals
    };
    string inventoryJson = JsonUtility.ToJson(saveObjectInventory);

    SaveSystem.Save(inventoryJson, "save_inventory");
  }

  private void SaveEnemies() {
    string enemiesJson = "";

    foreach (GameObject enemy in enemyGameObjects) {
      EnemyInterface enemyI = enemy.GetComponent<EnemyInterface>();

      Vector2 enemyPosition = enemyI.GetPosition();
      Vector3 enemyScale = enemyI.GetScale();
      Color enemyColor = enemyI.GetColor();
      float enemySpeed = enemyI.GetSpeed();
      float enemyHealth = enemyI.GetHealth();
      float enemyBasicDMG = enemyI.GetBasicDMG();
      float enemyBasicSpeed = enemyI.GetBasicSpeed();

      SaveObjectEnemy saveObjectEnemy = new SaveObjectEnemy {
        enemyPosition = enemyPosition,
        enemyScale = enemyScale,
        enemyColor = enemyColor,
        enemySpeed = enemySpeed,
        enemyHealth = enemyHealth,
        enemyBasicDMG = enemyBasicDMG,
        enemyBasicSpeed = enemyBasicSpeed
      };

      enemiesJson += "ENEMY" + JsonUtility.ToJson(saveObjectEnemy);
    }

    SaveSystem.Save(enemiesJson, "save_enemies");
  }

  private bool LoadPlayer() {
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

      Debug.Log("Player Save Loaded!");
      return true;
    } else {
      Debug.Log("No Player Save Exists!");
      return false;
    }
  }

  private void LoadTrees() {
    string saveString = SaveSystem.Load("save_trees");

    if (saveString != null) {
      // destroy existing trees
      foreach (GameObject t in GameObject.FindGameObjectsWithTag("TreeParent")) {
        Destroy(t);
      }

      // recreate trees using saved data
      string[] treesSplit = saveString.Split(new string[] { "TREE" }, StringSplitOptions.None);

      List<string> eachTree = new List<string>();
      foreach (string s in treesSplit) {
        if (s != "") {
          eachTree.Add(s);
        }
      }

      foreach (string s in eachTree) {
        SaveObjectTree saveObjectTree = JsonUtility.FromJson<SaveObjectTree>(s);

        GameObject treePrefab = GameObject.FindGameObjectWithTag("TreeOrigin");
        GameObject clone = Instantiate(treePrefab, saveObjectTree.treePosition, Quaternion.identity) as GameObject;
        clone.tag = "TreeParent";
        TreeInterface treeI = clone.GetComponentInChildren<TreeInterface>();

        treeI.SetPosition(saveObjectTree.treePosition);
        treeI.SetHealth(saveObjectTree.treeHealth);
        treeI.SetAge(saveObjectTree.treeAge);
      }
      Debug.Log("Trees Save Loaded!");
    } else {
      Debug.Log("No Trees Save Exists!");
    }
    
  }

  private void LoadInventory() {
    string saveString = SaveSystem.Load("save_inventory");
    SaveObjectInventory saveObjectInventory = JsonUtility.FromJson<SaveObjectInventory>(saveString);

    if (saveString != null) {
      //empty inventory
      inventory.EmptyInventory();

      int inventorySize = saveObjectInventory.keys.Count;

      for (int i = 0; i < inventorySize; i++) {
        inventory.AddItem(saveObjectInventory.keys[i], saveObjectInventory.vals[i]);
      }

      Debug.Log("Inventory Save Loaded!");
    } else {
      Debug.Log("No Inventory Save Exists!");
    }
  }

  private void LoadEnemies() {
    string saveString = SaveSystem.Load("save_enemies");
    
    if (saveString != null) {
      // destroy existing enemies
      foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy")) {
        Destroy(e);
      }

      // recreate enemies using saved data
      string[] enemiesSplit = saveString.Split(new string[] { "ENEMY" }, StringSplitOptions.None);

      List<string> eachEnemy = new List<string>();
      foreach (string s in enemiesSplit) {
        if (s != "") {
          eachEnemy.Add(s);
        }
      }

      foreach (string s in eachEnemy) {
        SaveObjectEnemy saveObjectEnemy = JsonUtility.FromJson<SaveObjectEnemy>(s);

        GameObject enemyPrefab = GameObject.FindGameObjectWithTag("EnemyOrigin");
        GameObject clone = Instantiate(enemyPrefab, saveObjectEnemy.enemyPosition, Quaternion.identity) as GameObject;
        clone.tag = "Enemy";
        EnemyInterface enemyI = clone.GetComponent<EnemyInterface>();

        enemyI.SetPosition(saveObjectEnemy.enemyPosition);
        enemyI.SetScale(saveObjectEnemy.enemyScale);
        enemyI.SetColor(saveObjectEnemy.enemyColor);
        enemyI.SetSpeed(saveObjectEnemy.enemySpeed);
        enemyI.SetHealth(saveObjectEnemy.enemyHealth);
        enemyI.SetBasicDMG(saveObjectEnemy.enemyBasicDMG);
        enemyI.SetBasicSpeed(saveObjectEnemy.enemyBasicSpeed);
      }
      
      Debug.Log("Enemy Save Loaded!");
    } else {
      Debug.Log("No Enemies Save Exists!");
    }
  }

  private class SaveObjectPlayer {
    public Vector2 playerPosition;
    public float playerSpeed;
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public float playerSwingSpeed;
    public float playerRPS;
    public float playerDMG;
  }

  private class SaveObjectTree {
    public Vector2 treePosition;
    public float treeHealth;
    public Age treeAge;
  }

  private class SaveObjectInventory {
    public List<string> keys;
    public List<int> vals;
  }

  private class SaveObjectEnemy {
    public Vector2 enemyPosition;
    public Vector3 enemyScale;
    public Color enemyColor;
    public float enemySpeed;
    public float enemyHealth;
    public float enemyBasicDMG;
    public float enemyBasicSpeed;
  }

}
