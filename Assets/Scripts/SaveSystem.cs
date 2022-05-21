using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveEnemies (GameObject[] enemies)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/enemies.ast";
        FileStream stream = new FileStream(path, FileMode.Create);

        EnemyData data = new EnemyData(enemies);

        formatter.Serialize(stream, data);
        Debug.Log("Saved enemies.ast");
        stream.Close();
    }

    public static EnemyData LoadEnemies()
    {
        string path = Application.dataPath + "/enemies.ast";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();

            Debug.Log("Loaded enemies.ast");
            return data;

        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SavePlayer (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/player.ast";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        Debug.Log("Saved player.ast");
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.dataPath + "/player.ast";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("Loaded player.ast");
            return data;

        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
