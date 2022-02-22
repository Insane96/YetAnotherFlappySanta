using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public string text = "Score: {0}";

    void Start()
    {
        ScoreController.instance.OnScoreChanged.AddListener(UpdateText);
    }

    void UpdateText(int score)
    {
        this.GetComponent<Text>().text = string.Format(text, score);
    }
}
