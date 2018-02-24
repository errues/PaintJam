using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyMovement : EnemyMovement {
	void Start () {
		base.Initialize ();	
	}

	void Update () {		
		base.Update ();

		if (ShouldChase ()) {
			float xVelocity = Mathf.Clamp(speed * (player.position.x - transform.position.x), -speed, speed);
			rb.velocity = new Vector2 (xVelocity, rb.velocity.y);
		}
	}

	protected override bool ShouldChase(){
		return Mathf.Abs (player.position.x - transform.position.x) < chasingDistance &&
		Mathf.Abs (player.position.y - transform.position.y) < maxHeightDifference;
	}
}
