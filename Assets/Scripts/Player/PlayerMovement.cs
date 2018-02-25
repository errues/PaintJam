using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Range(2, 5)]
    public float speed;
    [Range(2, 10)]
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private Vector2 xVel;
    private bool inContactWithWall;
    private Vector2 jumpWallDir;
    private bool grounded;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        xVel = Vector2.zero;
        grounded = false;
        inContactWithWall = false;
    }

    // Update is called once per frame
    void Update() {
        moveHorizontal();
        fall();
        jump();
        wallJump();
        if (grounded && inContactWithWall) {
            inContactWithWall = false;
            jumpWallDir = Vector2.zero;
        }
    }

    private void moveHorizontal() {
        if (!inContactWithWall) {
            float xVelocity = Mathf.Clamp(speed * Input.GetAxis("Horizontal"), -speed, speed);
            xVel.x = xVelocity;
        }
        xVel.y = rb.velocity.y;
        rb.velocity = xVel;
    }

    private void fall() {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void jump() {
        if (Input.GetButtonDown("Jump") && grounded && !inContactWithWall) {
            grounded = false;
            xVel = Vector2.up * jumpForce;
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void wallJump() {
        if (Input.GetButtonDown("Jump") && !grounded && inContactWithWall) {
            xVel = (Vector2.up + jumpWallDir) * jumpForce;
            print(xVel);
            rb.velocity += xVel;
            print(rb.velocity);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Border") || collision.gameObject.CompareTag("TriggerPlatform")) {
            foreach (ContactPoint2D contact in collision.contacts) {
                if (contact.normal.x == 1 || contact.normal.x == -1) {
                    inContactWithWall = true && !grounded; //if in air and touch a wall
                    jumpWallDir.x = contact.normal.x;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Platform") || collision.CompareTag("Border") || collision.CompareTag("TriggerPlatform")) {
            grounded = true;
        }
    }

}