using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public float fallMultiplier = 2.5f;

	protected Transform player;
	protected Rigidbody2D rb;

	private Collider2D myCollider;

	protected void Initialize(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		rb = GetComponent<Rigidbody2D> ();

		myCollider = GetComponent<Collider2D> ();
	}

	protected float GetZValue(){
		Vector3 pl = new Vector3 (player.position.x, player.position.y, 0);
		Vector3 me = new Vector3 (transform.position.x, transform.position.y, 0);
		return Mathf.Min (Vector3.Distance (pl, me), 50);
	}

	private void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (col.collider, myCollider);
		}
	}
}
