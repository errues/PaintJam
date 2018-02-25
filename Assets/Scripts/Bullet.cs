using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float speed = 15f;
	public SpriteRenderer filling;
	public SpriteRenderer border;
	public float timeUntilDestroy = 1f;

	public AudioClip[] hitClip;
	public AudioClip[] failClip;
	public AudioClip[] wallClip;

	private BasicColors color;
	private int damage;

	private Vector3 direction;
	private AudioSource audioSource;

	public void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

	public void Initialize(BasicColors color, int damage, bool left){
		this.color = color;
		this.damage = damage;
		SetColor ();

		if (left) {
			direction = -Vector3.right;
		} else {
			direction = Vector3.right;
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
	}

	private void Update(){
		transform.Translate (direction * speed * Time.deltaTime);	
	}

	private void SetColor(){
		switch (color) {
		case BasicColors.AMARILLO:
			filling.color = Color.yellow;
			break;
		case BasicColors.AZUL:
			filling.color = Color.blue;
			break;
		case BasicColors.ROJO:
			filling.color = Color.red;
			break;
		}
	}

	public BasicColors getBulletColor(){		
		return color;
	}

	public int getDamage(){		
		return damage;
	}

	private void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Enemy") {
			if (col.gameObject.GetComponent<EnemyHealth> ().IsDead ()) {
				Physics2D.IgnoreCollision (col.collider, GetComponent<BoxCollider2D> ());
			} else {
				audioSource.Stop ();
				if (col.gameObject.GetComponent<EnemyHealth> ().Hit (color, damage)) {
					audioSource.PlayOneShot (hitClip [Random.Range (0, hitClip.Length)]);
				} else {
					audioSource.PlayOneShot (failClip [Random.Range (0, failClip.Length)]);
				}
				Destroy ();
			}
		} else if (col.gameObject.tag == "Border" || col.gameObject.tag == "Platform" || col.gameObject.tag == "TriggerPlatform") {
			audioSource.Stop ();
			AudioClip a = wallClip [Random.Range (0, wallClip.Length)];
			print (a);
			audioSource.PlayOneShot (a);
			Destroy ();
		} else if (col.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision (col.collider, GetComponent<Collider2D> ());
		}
	}

	private void Destroy(){
		filling.enabled = false;
		border.enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
		Object.Destroy (this.gameObject, timeUntilDestroy);
	}
}
