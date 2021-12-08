using UnityEngine;
using UnityEngine.UI;

public class RawHighScore : MonoBehaviour
{
    [SerializeField] Text _nameText;
    [SerializeField] Text _scoreText;

    private int _score = 0;

    [SerializeField] int _pos;

    

    public void SetUp(int score,string name)
    {
        _nameText.text = PlayerPrefs.GetString(_pos.ToString(),name);
        _score = PlayerPrefs.GetInt(_pos.ToString(),score);

        _scoreText.text = _score.ToString();
    }

}
