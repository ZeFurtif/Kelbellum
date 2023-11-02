using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance { get; private set; }
    [Header("Save Management")]
    private string saveDir = @"/saves";
    public SaveData data = new SaveData();

    [Header("First Load Characters Data")]
    public List<Character> charactersData = new List<Character>();

    [Header("Static Characters Data")]
    public AllCharacters allCharactersData;

    void Start()
    {
        Debug.Log(Application.persistentDataPath + saveDir);
    }

    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another singleton!");
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    public void CreateJson(SaveData targetData, int saveIndex)
    {
        targetData.LoadCharacters(charactersData);
        OnSaveNewData(targetData, saveIndex);

        string dataPath = JsonUtility.ToJson(targetData);
        File.WriteAllText(Application.persistentDataPath + saveDir + @"\save" + saveIndex + ".json", dataPath);

        Debug.Log("DataManager -- Created - " + dataPath);
    }

    public void SaveJson(SaveData targetData, int saveIndex)
    {
        targetData.ClearTempData();
        OnSaveNewData(targetData, saveIndex);

        string dataPath = JsonUtility.ToJson(targetData);
        File.WriteAllText(Application.persistentDataPath + saveDir + @"\save" + saveIndex + ".json", dataPath);

        Debug.Log("DataManager -- Saved - " + dataPath);
    }

    public void LoadJson(SaveData targetData, int saveIndex)
    {
        string dataPath = File.ReadAllText(Application.persistentDataPath + saveDir + @"\save" + saveIndex + ".json");
        JsonUtility.FromJsonOverwrite(dataPath, targetData);

        Debug.Log("DataManager -- Loaded - " + dataPath);
    }

    public void OnSaveNewData(SaveData targetData, int saveIndex)
    {
        targetData.saveIndex = saveIndex;

        DateTime now = DateTime.Now;
        targetData.saveDate = "" + now;
    }

}

[System.Serializable]
public class SaveData
{

    [Header("File Info")]
    public int saveIndex = -1;
    public string saveDate;

    [Header("Game Data")]
    public int coins = 0;
    public List<Character> characters = new List<Character>();

    [Header("Area Data")]
    public string currentArea = "1_Nohoak";
    public int spawnIndex = 0;

    [Header("Temp Data")]
    public List<Player> players = new List<Player>();


    public void ClearTempData()
    {
        players.Clear();
    }

    public void NewPLayer(int pI, string pCS, InputDevice pD)
    {
        Player newPlayer = new Player();
        newPlayer.index = pI;
        newPlayer.controlScheme = pCS;
        newPlayer.device = pD;

        players.Add(newPlayer); 
    }

    public void LoadCharacters(List<Character> newCharacters)
    {
        characters = newCharacters;
    }

    public bool CheckCoins(int coinAmount)
    {
        return (coins >= coinAmount);
    }

    public void AddCoins (int coinAmount)
    {
        coins += coinAmount;
    }

    public void SaveLevelData(string aN, int sI)
    {
        currentArea = aN;
        spawnIndex = sI;
    }
}

[System.Serializable]
public class Character
{
    public string characterName;
    public bool unlocked = false;
    public int xp = 0;
}

[System.Serializable]
public class Player
{
    public int index;
    public string controlScheme;
    public InputDevice device;
    //
    public int characterIndex;
}


[System.Serializable]
public class Settings
{
    
}