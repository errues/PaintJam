using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperMovement : EnemyMovement {
	public float jumpForce = 5;

	private bool onGround;

	void Start () {
		base.Initialize ();
		onGround = true;
	}
	
	void Update () {
		base.Update ();

		if (ShouldChase ()) {
			float xVelocity = Mathf.Clamp(speed * (player.position.x - transform.position.x), -speed, speed);
			rb.velocity = new Vector2 (xVelocity, rb.velocity.y);

			if (onGround) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
			}
		}
	}

	protected override bool ShouldChase(){
		return Mathf.Abs (player.position.x - transform.position.x) < chasingDistance &&
			Mathf.Abs (player.position.y - transform.position.y) < maxHeightDifference;
	}

	private void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D (col);
		if (col.gameObject.tag == "Border") {
			onGround = true;
		}
	}

	private void OnCollisionExit2D (Collision2D col) {		
		if (col.gameObject.tag == "Border") {
			onGround = false;
		}	
	}
}
