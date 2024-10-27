using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Mathematics;
using UnityEngine;

public interface ISaveable{
    SaveData Save();
    void Load(SaveData data);
}

[System.Serializable]
public class SaveData{
    public Vector2 position;
    public float speed;
}

[System.Serializable]
public class ScoreData{
    public int score;

    public ScoreData(int score)
    {
        this.score = score;
    }
}

public class TransformSaver : MonoBehaviour
{
    [Header("File Paths")]
    private string jsonPath;
    private string binaryPath;

    [Header("Enemy Prefab")]
    public GameObject enemy;


    // Singleton setup
    private void Awake() {
        jsonPath = Application.persistentDataPath + "/transformsave.json";
        binaryPath = Application.persistentDataPath + "/scoresave.dat";
    }
#region JSON SAVING
    // Save our transforms.
    public void SaveTransforms(){
        // Make a list for our save data.
        List<SaveData> saveDataList = new List<SaveData>();
        SaveData playerSave = new SaveData();


        // Find all of our saveables.
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        // Save each of these objects to our saveDataList.
        foreach(var saveable in allSaveableObjects)
        {
            // Determine if we are saving the player or an enemy.
            Enemy enemyScript = ((MonoBehaviour)saveable).GetComponent<Enemy>();

            // We are saving the player.
            if(enemyScript == null)
            {
                PlayerManager playerScript = ((MonoBehaviour)saveable).GetComponent<PlayerManager>();
                playerSave.position = ((MonoBehaviour)saveable).transform.position;
                playerSave.speed = 5;
                continue;
            }

            // We are saving an enemy.
            SaveData thisData = new SaveData();
            thisData.position = ((MonoBehaviour)saveable).transform.position;
            thisData.speed = ((MonoBehaviour)saveable).GetComponent<Enemy>().moveSpeed;
            saveDataList.Add(thisData);
            continue;
        }

        // Add our player to the end of the list.
        saveDataList.Add(playerSave);

        // Serialize to JSON. Convert list to array to work with JSON.
        SaveData[] saveDataArray = saveDataList.ToArray();
        
        string jsonData = JsonUtility.ToJson(new ArrayWrapper {items = saveDataArray});
        File.WriteAllText(jsonPath, jsonData);

        print("Saved " + saveDataArray.Length.ToString() + " objects!");
    }

    // Wrapper class so JsonUtility can work with arrays.
    [System.Serializable]
    private class ArrayWrapper
    {
        public SaveData[] items;
    }

    // Load our transforms!
    public void LoadTransforms(){
        // Determine if our file exists.
        if(!File.Exists(jsonPath))
            return;

        // Read our json file.
        string jsonData = File.ReadAllText(jsonPath);

        // Deserialize into a SaveData array.
        ArrayWrapper wrapper = JsonUtility.FromJson<ArrayWrapper>(jsonData);

        // Delete all of our enemies.
        DeleteAllEnemies();

        // Restore our enemies and player position.
        RestoreTransforms(wrapper.items);

    }

    // Delete all of our current enemies.
    private void DeleteAllEnemies(){
        // Find all of our saveables.
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        // Delete all besides the player.
        foreach(var saveable in allSaveableObjects)
        {
            PlayerManager playerScript = ((MonoBehaviour)saveable).GetComponent<PlayerManager>();

            if(playerScript != null)
                continue;

            print("destroy enemy!" + ((MonoBehaviour)saveable).name);
            Destroy(((MonoBehaviour)saveable).gameObject);
        }
    }

    // Restore our enemies.
    private void RestoreTransforms(SaveData[] data){
        for(int i=0; i<data.Length; i++)
        {
            // If we are dealing with the player...
            if(i == data.Length - 1)
            {
                // Restore the players position. That is all!
                transform.position = data[i].position;
                continue;
            }

            // If we are dealing with an enemy...
            // Create an enemy at the appropriate location.
            GameObject thisEnemy = Instantiate(enemy, data[i].position, quaternion.identity);

            // Give this enemy the correct speed.
            thisEnemy.GetComponent<Enemy>().moveSpeed = data[i].speed;
        }
    }
#endregion

#region BINARY SAVING
    // Save our score.
    public void SaveScore(){
        // Get our score.
        int score = GetComponent<PlayerManager>().totalScore;

        // Create our score data and binary formatter.
        ScoreData scoreData = new ScoreData(score);
        BinaryFormatter formatter = new BinaryFormatter();

        // Use our binary path to serialize our data.
        using (FileStream file = File.Create(binaryPath))
        {
            formatter.Serialize(file, scoreData);
        }
    }

    // Load our score.
    public void LoadScore(){
        // Determine if our file exists.
        if(!File.Exists(binaryPath))
            return;

        // Create our binary formatter.
        BinaryFormatter formatter = new BinaryFormatter();

        // Open the file and deserialize.
        using (FileStream file = File.Open(binaryPath, FileMode.Open))
        {
            ScoreData scoreData = (ScoreData)formatter.Deserialize(file);

            // Apply to our current score.
            GetComponent<PlayerManager>().SetScore(scoreData.score);
        }
    }

#endregion
}


