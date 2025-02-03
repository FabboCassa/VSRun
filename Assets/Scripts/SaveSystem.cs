using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

public static class SaveSystem
{
    private static DataSerializer savedData;
    private static Data data;

    public static void initializeData()
    {
        if (LoadData() == null)
        {
            Data data=new Data();
            SaveData(data);
        }
        savedData = LoadData();
        data = savedData.GetData();
    }

    public static Data getSavedData()
    {
        return data;
    }

    public static void SaveData(Data dataToSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/vsrun.data"; //persistentDataPath is a directory that is unique to your game and it is not deleted when the game is uninstalled
        FileStream stream = new FileStream(path, FileMode.Create);

        DataSerializer data = new DataSerializer(dataToSave);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataSerializer LoadData()
    {
        string path = Application.persistentDataPath + "/vsrun.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Debug.Log("File loaded from: " + path);

            DataSerializer data = formatter.Deserialize(stream) as DataSerializer;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
