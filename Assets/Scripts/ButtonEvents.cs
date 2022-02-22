using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public void StartGame()
    {
        GameControl.instance.StartGame();
    }
    public void ChangeDifficulty()
    {
        GameControl.instance.LoopDifficulty();
    }
}
