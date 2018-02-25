using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideExplode : MonoBehaviour {
	public float explosionTime = 0.5f;
	public AudioClip explosionClip;

	public Sprite explosionBorder;
	public Sprite explosionFilling;

	private AudioSource audioSource;

	private void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

	public void Explode(){
		GetComponent<EnemySpriteManager> ().border.sprite = explosionBorder;
		GetComponent<EnemySpriteManager> ().filling.sprite = explosionFilling;

		audioSource.PlayOneShot (explosionClip);

		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
		Object.Destroy (this.gameObject, explosionTime);
	}
}
