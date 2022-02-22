using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    private bool gameOver;
    public bool GameOver
    {
        get { return gameOver; }
        set { 
            gameOver = value;
            OnGameStateUpdate.Invoke(value);
        }
    }
    public UnityEvent<bool> OnGameStateUpdate { get; private set; } = new UnityEvent<bool>();

    public float timeSinceDeath = 0f;

    public AudioClip bonkClip;
    public AudioClip scoreClip;
    private AudioSource audioSource;

    public Text difficultyText;
    public int difficulty { get; private set; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();

        this.difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        this.difficultyText.text = Difficulty.difficulties[this.difficulty].Name;

        SceneManager.sceneLoaded += delegate (Scene scene, LoadSceneMode arg1)
        {
            this.GameOver = false;
            if (scene.name == "Game")
            {
                this.GetComponent<ColumnPool>().Init();
                this.GetComponent<ColumnPool>().enabled = true;
            }
            else if (scene.name == "Main")
            {
                this.GetComponent<ColumnPool>().enabled = false;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        TryRestartGame();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Game")) && this.GameOver)
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
            else if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Main")))
                Application.Quit();
        }
    }

    private void TryRestartGame()
    {
        if (this.GameOver)
        {
            this.timeSinceDeath += Time.deltaTime;
            if (this.timeSinceDeath > 1f)
            {
                //gameOverText.transform.GetChild(0).gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    this.StartGame();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void SantaScored()
    {
        if (this.GameOver)
            return;
        ScoreController.instance.Score += 1;//Difficulty.difficulties[this.difficulty].ScorePerColumn;
        this.audioSource.clip = scoreClip;
        this.audioSource.Play();
    }

    public void SantaDied()
    {
        this.GameOver = true;
        this.audioSource.clip = bonkClip;
        this.audioSource.Play();
        ScoreController.instance.OnDeath();
    }

    public void LoopDifficulty()
    {
        this.difficulty++;
        if (this.difficulty >= Difficulty.difficulties.Length)
            this.difficulty = 0;

        PlayerPrefs.SetInt("Difficulty", this.difficulty);
        difficultyText.text = Difficulty.difficulties[this.difficulty].Name;
    }

    public Vector2 ScrollSpeed = new Vector2(-2f, 0f);
    public float GetColumnSpawnRate() => Difficulty.difficulties[this.difficulty].ColumnSpawnRate;

    public class Difficulty
    {
        public static readonly Difficulty Easy = new Difficulty("Easy", 2, 2.5f);
        public static readonly Difficulty Normal = new Difficulty("Normal", 3, 2f);
        public static readonly Difficulty Hard = new Difficulty("Hard", 5, 1.5f);

        public static readonly Difficulty[] difficulties = new Difficulty[] { Easy, Normal, Hard };

        public string Name { get; }
        public int ScorePerColumn { get; }
        public float ColumnSpawnRate { get; }

        private Difficulty(string name, int scorePerColumn, float columnSpawnRate)
        {
            this.Name = name;
            this.ScorePerColumn = scorePerColumn;
            this.ColumnSpawnRate = columnSpawnRate;
        }
    }
}
