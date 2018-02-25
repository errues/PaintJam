using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnedMovement : EnemyMovement {
	public float stopChasingDistance = 10;
	public float timeBetweenShots = 5;
	public float maxHeighDifferenceForShooting = 1.5f;
	public float gunHeight = 1.5f;

	public GameObject enemyBullet;

	public AudioClip[] shotClips;

	private float lastShotTime;
	private AudioSource audioSource;

	void Start () {
		base.Initialize ();
		lastShotTime = Time.time;
		audioSource = GetComponent<AudioSource> ();
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
		bool shoot = false;
		if (Time.time - lastShotTime > timeBetweenShots) {
			if (Mathf.Abs (player.position.y - transform.position.y) <= maxHeighDifferenceForShooting) {
				Vector3 origin = new Vector3 (transform.position.x, transform.position.y + gunHeight, 0);
				Vector3 target = new Vector3 (player.position.x, transform.position.y + gunHeight, 0);
				RaycastHit2D[] hits = Physics2D.RaycastAll (origin, target - origin);
				foreach (RaycastHit2D h in hits) {
					if (h.collider != null) {
						if (h.collider.gameObject.tag == "Player") {
							shoot = true;
						}
					}
				}
			}			
		}
		return shoot;
	}

	private void Shoot(){
		lastShotTime = Time.time;
		bool left = player.transform.position.x < transform.position.x;
		float x = 1f;
		if (left)
			x = -1f;
		Transform bullet = Object.Instantiate (enemyBullet, new Vector3(transform.position.x + x, transform.position.y + gunHeight, -1), new Quaternion()).transform;
		bullet.GetComponent<EnemyBullet>().Initialize(left);

		int r = Random.Range (0, shotClips.Length);
		audioSource.PlayOneShot (shotClips [r]);
	}
}
