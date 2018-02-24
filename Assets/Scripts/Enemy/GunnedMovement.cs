using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnedMovement : EnemyMovement {
	public float stopChasingDistance = 10;
	public float timeBetweenShots = 5;
	public float maxHeighDifferenceForShooting = 1.5f;

	public GameObject enemyBullet;

	private float lastShotTime;

	void Start () {
		base.Initialize ();
		lastShotTime = Time.time;
	}

	new void Update () {		
		base.Update ();

		if (rb.velocity.x == 0 && ShouldShoot ()) {
			Shoot ();
		}

		if (ShouldChase ()) {
			float xVelocity = Mathf.Clamp(speed * (player.position.x - transform.position.x), -speed, speed);
			rb.velocity = new Vector2 (xVelocity, rb.velocity.y);
		} 
	}

	protected override bool ShouldChase(){
		return Mathf.Abs (player.position.x - transform.position.x) < chasingDistance &&
			Mathf.Abs (player.position.x - transform.position.x) > stopChasingDistance &&
			Mathf.Abs (player.position.y - transform.position.y) < maxHeightDifference;
	}

	private bool ShouldShoot(){
		if (Time.time - lastShotTime > timeBetweenShots) {
			if (Mathf.Abs (player.position.y - transform.position.y) <= maxHeighDifferenceForShooting) {
				Debug.DrawRay(transform.position, player.position - transform.position);
				RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, player.position - transform.position);
				if (hits.Length > 1) {
					if (hits[1].collider != null) {
						if (hits[1].collider.gameObject.tag == "Player") {
							print ("FUEGO!");
							return true;
						}
					}
				}
			}			
		}
		return false;
	}

	private void Shoot(){
		lastShotTime = Time.time;
		Transform bullet = Object.Instantiate (enemyBullet, new Vector3(transform.position.x, player.position.y + 1f, -1), new Quaternion()).transform;
		Vector3 dir = Vector3.right;
		if (player.transform.position.x < transform.position.x)
			dir = -Vector3.right;
		bullet.GetComponent<EnemyBullet>().SetDirection(dir); 
	}
}
