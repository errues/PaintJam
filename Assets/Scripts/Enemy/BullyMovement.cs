using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyMovement : EnemyMovement {
	public float speed = 5;
	public float chasingDistance = 5;
	public float maxHeightDifference = 0.5f;

	void Start () {
		base.Initialize ();	
	}

	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, GetZValue ());

		if (ShouldChase ()) {
			float xVelocity = Mathf.Clamp(speed * (player.position.x - transform.position.x), -speed, speed);
			rb.velocity = new Vector2 (xVelocity, rb.velocity.y);
		}

		if (rb.velocity.y < 0) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
	}

	private bool ShouldChase(){
		print (player.position.x + " - " + transform.position.x + " - " + player.position.y + " - " + transform.position.y);
		return Mathf.Abs (player.position.x - transform.position.x) < chasingDistance &&
		Mathf.Abs (player.position.y - transform.position.y) < maxHeightDifference;
	}
}
