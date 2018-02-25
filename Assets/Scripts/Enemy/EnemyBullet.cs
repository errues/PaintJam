using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	public float speed = 2f;

	private Vector3 targetDirection;
	private Collider2D myCollider;

	public void Initialize(bool left){
		if (left)
			targetDirection = -Vector3.right;
		else
			targetDirection = Vector3.right;
		myCollider = GetComponent<Collider2D> ();
	}

	private void Update(){
		transform.Translate (targetDirection * speed * Time.deltaTime);
	}

	private void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (col.collider, myCollider);
		} else if (col.gameObject.tag == "Player") {
			// Aquí llamaremos a playerHealth
			Object.Destroy (this.gameObject);
		} else if (col.gameObject.tag == "Border") {
			Object.Destroy (this.gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D col){
		print (col.gameObject.tag);
	}
}
