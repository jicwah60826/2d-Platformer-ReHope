using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public SaveData activeSave;

    public string sceneToNotSave;

    private void Awake()
    {
        SetupInstance();
    }

    public void SetupInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        Debug.Log("Saving Data");
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/Dash.save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
    }
    public void Load()
    {
        Debug.Log("Loading Data");
        string dataPath = Application.persistentDataPath;
        if (File.Exists(dataPath + "/Dash.save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/Dash.save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
        else
        {
            Debug.LogWarning("No save data to load");
        }
    }

    public void DestroySaveSystem()
    {
        // no save system is assigned as an instance
        instance = null;

        //destroy the save system game object
        Destroy(gameObject);
    }
}
