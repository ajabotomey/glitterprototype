using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Saves/player.kwf";
        FileInfo file = new FileInfo(path);
        file.Directory.Create();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Saves/player.kwf";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Saves/player_settings.kwf";
        FileInfo file = new FileInfo(path);
        file.Directory.Create();
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/Saves/player_settings.kwf";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
