using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnedMovement : EnemyMovement {
	public float stopChasingDistance = 10;

	void Start () {
		base.Initialize ();
	}

	new void Update () {		
		base.Update ();

		if (ShouldChase ()) {
			float xVelocity = Mathf.Clamp (speed * (player.position.x - transform.position.x), -speed, speed);
			rb.velocity = new Vector2 (xVelocity, rb.velocity.y);
		}
	}

	protected override bool ShouldChase(){
		bool x = Mathf.Abs (player.position.x - transform.position.x) < chasingDistance &&
			Mathf.Abs (player.position.x - transform.position.x) > stopChasingDistance &&
			Mathf.Abs (player.position.y - transform.position.y) < maxHeightDifference;
		return x;
	}
}
