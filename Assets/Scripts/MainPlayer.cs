using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour {

    [Range(2, 5)]
    public float speed;
    [Range(2, 10)]
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private bool isJumping;
    private Rigidbody2D rb;
    private Vector2 xVel;
    private Vector2 yVel;


    // Use this for initialization
    void Start () {
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
        xVel = Vector2.zero;
        yVel = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update () {
        move();
        jump();
	}

    private void move() {

        //xVel = rb.velocity.x * Vector2.right;
        //if (!isJumping) { //Only add the horizontal speed if the player is on the ground
        //    xVel = speed * Vector2.right * Input.GetAxis("Horizontal");
        //}

        //if (rb.velocity.y < 0) {
        //    yVel += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //} else if (rb.velocity.y > 0) {
        //    yVel += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}

        //rb.velocity = xVel + yVel;

        if (!isJumping) { //Only add the horizontal speed if the player is on the ground
            float xVelocity = Mathf.Clamp(speed * 1 * Input.GetAxis("Horizontal"), -speed, speed);
            xVel.x = xVelocity;
            
        }

        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        xVel.y = rb.velocity.y;
        rb.velocity = xVel;
        //rb.velocity = xVel + yVel;
    }

    private void jump() {
        if (Input.GetButtonDown("Jump") && !isJumping) {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void OnCollisionStay2D(Collision2D col) {
        if (!Input.GetButton("Jump")) { 
            isJumping = false;
        }
    }
}
