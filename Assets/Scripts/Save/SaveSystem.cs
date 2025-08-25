using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{
    private static SaveData _saveData = new SaveData();

    [System.Serializable]

    public struct SaveData
    {
        public ScoreSaveData scoreData;
        public SceneFruitData fruitData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }
    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleSaveData()
    {
        ScoreManager.instance.Save(ref _saveData.scoreData);
        FruitManager.instance.Save(ref _saveData.fruitData);
    }

    public static void ClearData()
    {
        ScoreManager.instance.ClearData(ref _saveData.scoreData);
        FruitManager.instance.ClearData(ref _saveData.fruitData);
        Save();
    }

    private static void HandleLoadData()
    {
        ScoreManager.instance.Load(_saveData.scoreData);
        FruitManager.instance.Load(_saveData.fruitData);
    }
}

