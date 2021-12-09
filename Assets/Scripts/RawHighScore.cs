using UnityEngine;
using UnityEngine.UI;

public class RawHighScore : MonoBehaviour
{
    [SerializeField] Text _nameText;
    [SerializeField] Text _scoreText;

    public PlayerData PlayerData;


    private void Start()
    {
        _nameText.text = PlayerData.Name;
        _scoreText.text = PlayerData.Score.ToString();
    }
}
