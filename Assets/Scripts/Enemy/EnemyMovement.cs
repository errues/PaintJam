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
	protected SpriteRenderer filling;
	protected SpriteRenderer border;

	private float fillXSize;
	private float borderXSize;

	private bool headingLeft;
	private bool headingChanged;

	protected EnemyHealth enemyHealth;

	protected void Initialize(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		rb = GetComponent<Rigidbody2D> ();

		myCollider = GetComponent<Collider2D> ();
		filling = GetComponent<EnemySpriteManager> ().filling;
		border = GetComponent<EnemySpriteManager> ().border;
		fillXSize = filling.transform.localScale.x;
		borderXSize = border.transform.localScale.x;

		headingChanged = false;
		headingLeft = true;

		enemyHealth = GetComponent<EnemyHealth> ();
	}

	protected void Update(){
		if (!enemyHealth.IsDead ()) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, GetZValue ());
			if (rb.velocity.y < 0) {
				rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
			}
			if (rb.velocity.x > 0 && headingLeft == true) {
				headingLeft = false;
				headingChanged = true;
			} else if (rb.velocity.x < 0 && headingLeft == false) {
				headingLeft = true;
				headingChanged = true;
			}
			if (headingChanged) {
				headingChanged = false;
				SetSpriteOrientation (headingLeft);
			}
		}
	}

	public void SetSpriteOrientation(bool heading){
		headingLeft = heading;
		if (headingLeft) {
			filling.transform.localScale = new Vector3 (fillXSize, filling.transform.localScale.y, 1);
			border.transform.localScale = new Vector3 (borderXSize, border.transform.localScale.y, 1);
		} else {
			filling.transform.localScale = new Vector3 (-fillXSize, filling.transform.localScale.y, 1);
			border.transform.localScale = new Vector3 (-borderXSize, border.transform.localScale.y, 1);

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
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ().takeDamage ();
			Physics2D.IgnoreCollision (col.collider, myCollider);
		}
	}
}
