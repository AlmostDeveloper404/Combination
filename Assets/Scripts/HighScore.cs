using UnityEngine;
using UnityEngine.SceneManagement;


public class HighScore : Singleton<HighScore>
{
    [SerializeField] RawHighScore[] allRaws;

    [HideInInspector] public int Score;
    [HideInInspector] public string Name;

    public void SetResult(int score, string name)
    {
        Score = score;
        Name = name;

        PlayerData[] playerDatas = new PlayerData[10];
        for (int i = 0; i < allRaws.Length; i++)
        {

            allRaws[i].PlayerData.Name = name;
            allRaws[i].PlayerData.Score = score;
        }
        
    }

    [ContextMenu("ClearPlayerPrefs")]
    void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
