using UnityEngine;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{
    [SerializeField] GameObject _blockPanal;
    private Text _timerText;
    private float _time;

    bool isGameOver = false;
    bool isStartedPlaying = false;

    private void Start()
    {
        _timerText = GetComponent<Text>();
    }

    public void StartTimer()
    {
        _blockPanal.SetActive(false);
        isStartedPlaying = true;
    }

    public void StopTimer()
    {
        _blockPanal.SetActive(true);
        isStartedPlaying = false;
    }

    public void SetTime(float time)
    {
        _time = time;
    }

    private void Update()
    {
        if (isGameOver || !isStartedPlaying) return;
        

        if (_time < 0)
        {
            Manager.Instance.GameOver();
            isGameOver = true;
        }
        


        _time -= Time.deltaTime;
        _timerText.text=$"Time: {_time.ToString("0")}";
    }
}
