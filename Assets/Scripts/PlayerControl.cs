using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private bool isDead = false;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private readonly Vector2 force = new Vector2(0f, 200f);

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(force);
            animator.SetTrigger("Flap");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;
        //rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.freezeRotation = false;
        rigidbody2D.AddTorque(50f);
        isDead = true;
        animator.SetTrigger("Dead");
        GameControl.instance.SantaDied();
    }
}
