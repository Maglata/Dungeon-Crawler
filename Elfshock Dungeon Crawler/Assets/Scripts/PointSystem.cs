using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PointSystem
{
    public static void SavePoints(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/points.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PointData data = new PointData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PointData LoadPoints()
    {
        string path = Application.persistentDataPath + "/points.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PointData data = formatter.Deserialize(stream) as PointData;
            stream.Close();

            return data;
        }
        else { return null; }
    }
}
