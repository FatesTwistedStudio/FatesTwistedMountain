using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Credit: https://www.youtube.com/watch?v=XOjd_qU2Ido&t=683s

public static class S_SaveSystem
{
    public static void SavePlayer(S_GameloopController playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        debugPath(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        S_PlayerData data = new S_PlayerData(playerData);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static S_PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            debugPath(path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            S_PlayerData data = formatter.Deserialize(stream) as S_PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }

    public static void debugPath(string pathName)
    {
        GameObject controller = GameObject.FindWithTag("GameController");
        if (controller != null)
        {
            controller.GetComponent<S_GameloopController>().SavePath = pathName;

        }
        else
        {
            controller = GameObject.FindWithTag("GameController");
        }
    }
}
