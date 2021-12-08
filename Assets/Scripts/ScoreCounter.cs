using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreCounter : Singleton<ScoreCounter>
{
    Text _scoreText;
    [SerializeField] int _startScore = 0;

    public UnityEvent RaiseDifficulty;

    int _currentScore = 0;

    private void Start()
    {
        _currentScore = _startScore;
        _scoreText = GetComponent<Text>();

        UpdateScore(_currentScore);
    }

    public void UpdateScore(int scoreAmount)
    {
        _currentScore += scoreAmount;
        if (_currentScore % 300 == 0)
        {
            RaiseDifficulty.Invoke();
        }
        _scoreText.text = $"Score: {_currentScore}";
    }

    public int GetScore() => _currentScore;
    
}
