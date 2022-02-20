using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static Vector2 scrollSpeed = new Vector2(-1.5f, 0f);

    public static GameControl instance;

    public GameObject gameOverText;
    public bool gameOver = false;
    public float timeSinceDeath = 0f;

    public Text scoreText;
    public Text hiScoreText;

    public AudioClip bonkClip;
    public AudioClip scoreClip;
    private AudioSource audioSource;

    public int score = 0;
    private int hiScore = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hiScore = PlayerPrefs.GetInt("HiScore", 0);
        hiScoreText.text = $"Best Score: {hiScore}";
    }

    // Update is called once per frame
    void Update()
    {
        RestartGame();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void RestartGame()
    {
        if (gameOver)
        {
            timeSinceDeath += Time.deltaTime;
            if (timeSinceDeath > 1f)
            {
                gameOverText.transform.GetChild(0).gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void BirdScored()
    {
        if (gameOver)
            return;
        score++;
        scoreText.text = $"Score: {score}";
        audioSource.clip = scoreClip;
        audioSource.Play();
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        gameOver = true;
        audioSource.clip = bonkClip;
        audioSource.Play();
        if (score > hiScore)
            PlayerPrefs.SetInt("HiScore", score);
    }
}
