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

    private Vector2 xVel;
    private Vector2 jumpWallDir;
    private bool inContactWithWall;
    private bool grounded;
    private bool jumpWall;
    private bool facingRight;
    private bool isCrouch;
    private float crouchSpeed;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerSounds sounds;
	private PlayerHealth playerHealth;

    // Use this for initialization
    void Start() {
        xVel = Vector2.zero;
        isCrouch = false;
        jumpWall = false;
        grounded = false;
        inContactWithWall = false;
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sounds = GetComponent<PlayerSounds>();
		playerHealth = GetComponent<PlayerHealth> ();
        crouchSpeed = speed * 0.5f;
    }

    // Update is called once per frame
    void Update() {
		if (!playerHealth.IsHidden ()) {
			moveHorizontal();
			crouch();
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
    }

    private void moveHorizontal() {
        if (!inContactWithWall && !jumpWall) {
            float xVelocity;
            if (isCrouch) {
                xVelocity = Mathf.Clamp(crouchSpeed * Input.GetAxis("Horizontal"), -crouchSpeed, crouchSpeed);

            } else {
               xVelocity = Mathf.Clamp(speed * Input.GetAxis("Horizontal"), -speed, speed);
            }
            xVel.x = xVelocity;
        }
        // TODO POSSIBLE add jump control with horizontal movement
        xVel.y = rb.velocity.y;
        rb.velocity = xVel;
    }

    private void crouch() {
        if (Input.GetAxis("Crouch") < 0) {
            isCrouch = true;
        } else {
            isCrouch = false;
        }
    }

    private void setAnimation() {
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Crouch", isCrouch);
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
        if (Input.GetButtonDown("Jump") && grounded && !inContactWithWall  && !isCrouch) {
            anim.SetTrigger("Jump");
            sounds.playJump();
            grounded = false;
            xVel = Vector2.up * jumpForce;
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void wallJump() {
        if (Input.GetButtonDown("Jump") && !grounded && inContactWithWall) {
            anim.SetTrigger("Jump");
            sounds.playJump();
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
  
	public bool GetFacing(){
		return facingRight;
	}
}