using UnityEngine;
using UnityEngine.UI;

public class LostPanal : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] InputField _inputField;
    [SerializeField] Button _continueButton;

    [SerializeField] GameObject _lostPanal;
    [SerializeField] GameObject _highScorePanal;

    private int _score;
    private string _name;

    private void Start()
    {
        SetScore();
    }

    public void SetScore()
    {
        _score = ScoreCounter.Instance.GetScore();
        _scoreText.text = $"Score: {_score}";
    }

    public void EnterName(string name)
    {
        _continueButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void Continue()
    {
        _highScorePanal.SetActive(true);

        _name = _inputField.text;
        HighScore.Instance.SetResult(_score,_name);

        _lostPanal.SetActive(false);
    }
}
