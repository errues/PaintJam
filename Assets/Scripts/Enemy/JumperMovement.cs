using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperMovement : EnemyMovement {
	public float jumpForce = 5;

	private bool onGround;
	private AudioSource audioSource;
	private EnemySounds enemySounds;

	void Start () {
		base.Initialize ();
		onGround = true;
		audioSource = GetComponent<AudioSource> ();
		enemySounds = GetComponent<EnemySounds> ();
	}
	
	new void Update () {
		base.Update ();

		if (ShouldChase ()) {
			float xVelocity = Mathf.Clamp(speed * (player.position.x - transform.position.x), -speed, speed);
			rb.velocity = new Vector2 (xVelocity, rb.velocity.y);

			if (onGround) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				onGround = false;
				audioSource.PlayOneShot (enemySounds.GetActionClip ());
			}
		}
	}

	protected override bool ShouldChase(){
		return Mathf.Abs (player.position.x - transform.position.x) < chasingDistance &&
			Mathf.Abs (player.position.y - transform.position.y) < maxHeightDifference;
	}

	new private void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D (col);
		if (col.gameObject.tag == "Border" || col.gameObject.tag == "Platform" || col.gameObject.tag == "TriggerPlatform") {
			onGround = true;
		}
	}
}
