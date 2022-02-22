using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = GameControl.instance.ScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.GameOver)
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}
