using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScore : Singleton<HighScore>
{
    [SerializeField]RawHighScore[] allRaws;

    public void SetResult(int score,string name)
    {

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(i.ToString())) continue;

            PlayerPrefs.SetInt(i.ToString(), score);
            PlayerPrefs.SetString(i.ToString(), name);
            
            allRaws[i].SetUp(score,name);
            UpdateAll();
            return;
        }
        int key = 0;
        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.GetInt(key.ToString()) > PlayerPrefs.GetInt(i.ToString()))
            {
                key = i;
            }
        }
        PlayerPrefs.SetInt(key.ToString(), score);
        PlayerPrefs.SetString(key.ToString(), name);


    }

    void UpdateAll()
    {
        for (int i = 0; i < 10; i++)
        {
            int score = PlayerPrefs.GetInt(i.ToString());
            string name = PlayerPrefs.GetString(i.ToString());
            allRaws[i].SetUp(score,name);
        }
    }

    //public void GetResult()
    //{
    //    //вопрос: в какую структуру лучше загружать сохранение,чтобы было удобно сортировать рейтинг?
    //    Dictionary<string, int> dicti = new Dictionary<string, int>();
    //    for (int i = 0; i < 10; i++)
    //    {
    //        dicti.Add(PlayerPrefs.GetString(i.ToString()), PlayerPrefs.GetInt(i.ToString()));
    //    }
    //    int[] arr = new int[10];
    //    for (int i = 0; i < 10; i++)
    //    {
    //        arr[i] = PlayerPrefs.GetInt(i.ToString());
            

    //    }
    //    //массив аrr надо отсортировать по возрастанию
    //    //такое создание словаря это тупо 
    //    Dictionary<string, int> sortDiction = new Dictionary<string, int>();
    //    for (int i = 0; i < 10; i++)
    //    {
    //        foreach (var item in dicti)
    //        {
    //            if (item.Value==arr[i])
    //            {
    //                sortDiction.Add(item.Key,item.Value);
    //                break;
    //            }
    //        }
    //    }
        
    //}

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
