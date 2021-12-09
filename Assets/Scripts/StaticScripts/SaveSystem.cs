using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string _path= "/higtscore.txt";

    public static void SaveProgress(HighScore highScore)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath +_path;
        FileStream fileStream = new FileStream(path,FileMode.Create);

        PlayerData playerData = new PlayerData(highScore);

        binaryFormatter.Serialize(fileStream,playerData);
        fileStream.Close();
    }

    public static PlayerData LoadProgress()
    {
        string path = Application.persistentDataPath + _path;
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path,FileMode.Open);

            PlayerData playerData= binaryFormatter.Deserialize(fileStream) as PlayerData;
            fileStream.Close();

            return playerData;
        }
        else
        {
            Debug.Log("File isn't found: "+ path);
            return null;
        }


    }
    
}
