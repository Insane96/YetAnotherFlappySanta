using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScoreUpdater : MonoBehaviour
{
    public string text = "Best Score: {0}";

    void Start()
    {
        ScoreController.instance.OnHiScoreChanged.AddListener(UpdateText);
        ScoreController.instance.LoadHighScore();
    }

    void UpdateText(int score)
    {
        this.GetComponent<Text>().text = string.Format(text, score);
    }
}
