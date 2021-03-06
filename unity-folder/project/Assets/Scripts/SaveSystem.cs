using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem {
  private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
  private const string SAVE_EXTENSION = ".txt";

  public static void Initialize() {
    // Check whether Save Folder already exists
    if (!Directory.Exists(SAVE_FOLDER)) {
      // Create a new Save Folder if one does not exist
      Directory.CreateDirectory(SAVE_FOLDER);
    }
  }

  public static void Save(string saveString, string fileName) {
    File.WriteAllText(SAVE_FOLDER + fileName + SAVE_EXTENSION, saveString);
  }

  public static string Load(string fileName) {
    if (File.Exists(SAVE_FOLDER + fileName + SAVE_EXTENSION)) {
      return File.ReadAllText(SAVE_FOLDER + fileName + SAVE_EXTENSION);
    } else {
      return null;
    }
  }
}
