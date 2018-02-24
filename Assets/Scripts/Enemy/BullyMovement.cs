using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyMovement : EnemyMovement {
	public float speed;

	void Start () {
		base.Initialize ();	
	}

	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, GetZValue ());

		float xVelocity = Mathf.Clamp(speed * (player.position.x - transform.position.x), -speed, speed);
		rb.velocity = new Vector2 (xVelocity, 0);
		if (rb.velocity.y < 0) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
	}
}
