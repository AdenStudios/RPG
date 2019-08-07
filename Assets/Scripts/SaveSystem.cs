using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static int saveSlots = 8;

    public static void SavePlayer(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save" + data.saveSlot;
        FileStream stream = new FileStream (path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(int slot)
    {
        string path = Application.persistentDataPath + "/save" + slot;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();
        return data;
    }

    public static List<PlayerData> LoadAllSaves()
    {
        List<PlayerData> saves = new List<PlayerData>();

        for (int i = 0; i < saveSlots; i++)
        {
            string path = Application.persistentDataPath + "/save" + i;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                saves.Add(data);
            }
        }
        return saves;
    }

    public static int GetFreeSaveSlotNumber()
    {
        for (int i = 0; i < saveSlots; i++)
        {
            string path = Application.persistentDataPath + "/save"+ i;
            if (File.Exists(path))
            {
                continue;
            }
            else
            {
                return i;
            }
        }
        return -1;
    }
}
