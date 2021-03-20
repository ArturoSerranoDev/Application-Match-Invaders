// ----------------------------------------------------------------------------
// SaveLoadController.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Handles saving and loading of data using JSON
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadController : UnitySingletonPersistent<SaveLoadController>
{
    const string fileName = "/gamesave.save";
    
    public void SaveHighScore(int score)
    {
        SaveFile saveFile = new SaveFile();
        saveFile.highScore = score;

        string json = JsonUtility.ToJson(saveFile);
        
        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        bf.Serialize(file, json);
        file.Close();
        
        Debug.Log("Saving as JSON " + json);
    }

    public int GetLatestHighScore()
    {
        if (!DoesSaveFileExist())
            return 0;
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
        string json = (string)bf.Deserialize(file);
        file.Close();
        
        return JsonUtility.FromJson<SaveFile>(json).highScore;
    }

    public bool DoesSaveFileExist()
    {
        return File.Exists(Application.persistentDataPath + fileName);
    }
}

[System.Serializable]
class SaveFile
{
    public int highScore;
}
