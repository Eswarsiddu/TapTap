using System.IO;
using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    private static string SAVE_PATH = "/saves";
    public static void Save<T>(T obj, DATAKEYS key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string directorypath = Application.persistentDataPath + SAVE_PATH;
        if (Directory.Exists(directorypath) == false)
        {
            Directory.CreateDirectory(directorypath);
        }
        string path = directorypath + "/" + Enum.GetName(typeof(DATAKEYS), key) + ".eswar";
        FileStream stream;

        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Truncate);
        }
        else
        {
            stream = new FileStream(path, FileMode.CreateNew);
        }
        formatter.Serialize(stream, obj);
        stream.Close();
    }

    public static T Load<T>(DATAKEYS key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVE_PATH + "/" + Enum.GetName(typeof(DATAKEYS), key) + ".eswar";
        T data = default;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            data = (T)formatter.Deserialize(stream);
            stream.Close();
        }
        return data;
    }
}
