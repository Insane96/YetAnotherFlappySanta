using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChanged.Invoke(value);
        }
    }

    public UnityEvent<int> OnScoreChanged { get; private set; } = new UnityEvent<int>();

    private int _hiScore;

    public int HiScore
    {
        get => _hiScore;
        set
        {
            _hiScore = value;
            OnHiScoreChanged.Invoke(value);
            PlayerPrefs.SetInt("HiScore", value);
        }
    }

    public UnityEvent<int> OnHiScoreChanged { get; private set; } = new UnityEvent<int>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Score = 0;
    }

    public void LoadHighScore()
    {
        this.HiScore = PlayerPrefs.GetInt("HiScore", 0);
    }

    public void OnDeath()
    {
        if (this.Score > this.HiScore)
            this.HiScore = this.Score;
    }
}
