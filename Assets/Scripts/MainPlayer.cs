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
    private bool inContactWithWall;
    private Vector2 jumpWallDir;
    private bool grounded;

    // Use this for initialization
    void Start () {
        grounded = false;
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
        xVel = Vector2.zero;
        inContactWithWall = false;
        jumpWallDir = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update () {
        move();
        jump();
        wallJump();
        print("groundeD: " + grounded);
    }

    private void move() {
        if (!isJumping && !inContactWithWall && grounded) { //Only add the horizontal speed if the player is on the ground
            float xVelocity = Mathf.Clamp(speed * Input.GetAxis("Horizontal"), -speed, speed);
            xVel.x = xVelocity;
        }

        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        xVel.y = rb.velocity.y;
        rb.velocity = xVel;
    }

    private void jump() {
        if (Input.GetButtonDown("Jump") && !isJumping && !inContactWithWall) {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    private void wallJump() {
        if (Input.GetButtonDown("Jump") && !isJumping && inContactWithWall) {
            isJumping = true;
            xVel = (Vector2.up + jumpWallDir) * jumpForce;
            rb.velocity += xVel;
        }
    }

    private void OnCollisionStay2D(Collision2D col) {
        Platform plat;
        if (!Input.GetButton("Jump")) { 
            isJumping = false;
        }
        if (plat = col.gameObject.GetComponent<Platform>()) {
            grounded = setGrounded(col.contacts);
            foreach (ContactPoint2D contact in col.contacts) {
                if (contact.normal.x == 1 || contact.normal.x == -1) {
                    inContactWithWall = true;
                    jumpWallDir.x = contact.normal.x;
                }
            }
            
            if (grounded) {
                inContactWithWall = false;
                jumpWallDir = Vector2.zero;
            }
        }
        
    }

    private bool setGrounded(ContactPoint2D[] contacts) {
        bool grounded = false;
        int i = 0;
        while(!grounded && i < contacts.Length) {
            grounded = contacts[i].normal.y == 1;
            i++;
        }
        return grounded;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (inContactWithWall) {
            inContactWithWall = false;
            jumpWallDir = Vector2.zero;
        }
    }
}
