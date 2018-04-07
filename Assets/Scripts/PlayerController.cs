using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        var xAxis = Input.GetAxis("Horizontal");
        var velocity = rb.velocity;

        if (xAxis != 0)
        {
            animator.SetBool("isMoving", true);
            velocity.x = 10f * xAxis;
            sr.flipX = xAxis < 0;
        } else
        {
            animator.SetBool("isMoving", false);
            velocity.x = 0f;
        }

        rb.velocity = velocity;
    }
}
