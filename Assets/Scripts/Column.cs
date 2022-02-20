using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerControl>() == null)
            return;
           
        GameControl.instance.BirdScored();
    }

    public void Init()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }
}
