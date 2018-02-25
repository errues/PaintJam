using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Range(2, 10)]
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
    private bool jumpWall;
    private bool facingRight;
    private Animator anim;

    // Use this for initialization
    void Start() {
        jumpWall = false;
        rb = GetComponent<Rigidbody2D>();
        xVel = Vector2.zero;
        grounded = false;
        inContactWithWall = false;
        facingRight = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        moveHorizontal();
        selectFacing();
        setAnimation();
        fall();
        jump();
        wallJump();
        if (jumpWall) {
            inContactWithWall = false;
            jumpWallDir = Vector2.zero;
        }
        if (grounded && jumpWall) {
            jumpWall = false;
        }
        
    }

    private void moveHorizontal() {
        if (!inContactWithWall && !jumpWall) {
            float xVelocity = Mathf.Clamp(speed * Input.GetAxis("Horizontal"), -speed, speed);
            xVel.x = xVelocity;
        }
        // TODO POSSIBLE add jump control with horizontal movement
        xVel.y = rb.velocity.y;
        rb.velocity = xVel;
    }

    private void setAnimation() {
        anim.SetBool("grounded", grounded);
        anim.SetBool("Moving", rb.velocity.x != 0);
    }

    private void selectFacing() {
        if(facingRight && rb.velocity.x < 0) {
            flip();
        }else if(!facingRight && rb.velocity.x > 0) {
            flip();
        }
    }

    private void flip() {
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
        facingRight = !facingRight;
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
            rb.velocity += xVel;
            jumpWallDir = Vector2.zero;
            jumpWall = true;
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
            inContactWithWall = false;
            if(collision.CompareTag("Platform") || collision.CompareTag("TriggerPlatform")) {
                Platform plat = collision.gameObject.GetComponent<Platform>();
                if (plat.type.Equals(Platform.PlatformType.Movable)) {
                    this.transform.SetParent(plat.transform);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (this.transform.parent) {
            this.transform.SetParent(null);
        }
    }
}