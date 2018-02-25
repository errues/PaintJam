using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideExplode : MonoBehaviour {
	public float explosionTime = 0.5f;

	public Sprite explosionBorder;
	public Sprite explosionFilling;

	private AudioSource audioSource;
	private EnemySounds enemySounds;

	private void Start(){
		audioSource = GetComponent<AudioSource> ();
		enemySounds = GetComponent<EnemySounds> ();
	}

	public void Explode(){
		GetComponent<EnemySpriteManager> ().border.sprite = explosionBorder;
		GetComponent<EnemySpriteManager> ().filling.sprite = explosionFilling;

		audioSource.PlayOneShot (enemySounds.GetActionClip());

		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
		Object.Destroy (this.gameObject, explosionTime);
	}
}
