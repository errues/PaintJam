using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour {
	public float speed = 5;
	public float chasingDistance = 5;
	public float maxHeightDifference = 0.5f;

	public float fallMultiplier = 2.5f;

	protected Transform player;
	protected Rigidbody2D rb;

	private Collider2D myCollider;

	protected void Initialize(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		rb = GetComponent<Rigidbody2D> ();

		myCollider = GetComponent<Collider2D> ();
	}

	protected void Update(){
		transform.position = new Vector3 (transform.position.x, transform.position.y, GetZValue ());
		if (rb.velocity.y < 0) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}

	}

	protected abstract bool ShouldChase ();

	private float GetZValue(){
		Vector3 pl = new Vector3 (player.position.x, player.position.y, 0);
		Vector3 me = new Vector3 (transform.position.x, transform.position.y, 0);
		return Mathf.Min (Vector3.Distance (pl, me), 50);
	}
	
	protected void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (col.collider, myCollider);
		} else if (col.gameObject.tag == "Player") {
			// Aquí llamaremos a playerHealth
			Physics2D.IgnoreCollision (col.collider, myCollider);
		}
	}
}
