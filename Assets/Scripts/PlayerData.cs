using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Score;
    public string Name;

    public PlayerData(HighScore highScore)
    {
        Score = highScore.Score;
        Name = highScore.Name;
    }

}
