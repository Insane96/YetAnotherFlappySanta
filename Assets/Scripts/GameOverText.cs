using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameControl.instance.OnGameStateUpdate.AddListener(UpdateGameOver);
    }

    void UpdateGameOver(bool gameOver)
    {
        this.GetComponent<Text>().enabled = gameOver;
    }
}
