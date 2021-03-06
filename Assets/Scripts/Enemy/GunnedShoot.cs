﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnedShoot : MonoBehaviour {
	public float gunHeight = 1.5f;
	public float gunX = 1.5f;
	public float timeBetweenShots = 5;
	public float maxShootDistance = 20;
	
	public GameObject enemyBullet;

	public Sprite shootSpriteBorder;
	public Sprite shootSpriteFill;
	private Sprite idleSpriteBorder;
	private Sprite idleSpriteFill;

	private SpriteRenderer filling;
	private SpriteRenderer border;

	private float lastShotTime;
	private Rigidbody2D rb;
	private Transform player;

	private AudioSource audioSource;
	private EnemySounds enemySounds;
	private EnemyHealth enemyHealth;

	void Start () {
		lastShotTime = Time.time;
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		audioSource = GetComponent<AudioSource> ();
		enemySounds = GetComponent<EnemySounds> ();
		enemyHealth = GetComponent<EnemyHealth> ();

		filling = GetComponent<EnemySpriteManager> ().filling;
		border = GetComponent<EnemySpriteManager> ().border;
		idleSpriteFill = filling.sprite;
		idleSpriteBorder = border.sprite;
	}
	
	void Update () {
		if (CanShoot () && !enemyHealth.IsDead ()) {
			// No muy eficiente, revisar
			filling.sprite = shootSpriteFill;
			border.sprite = shootSpriteBorder;
			GetComponent<EnemyMovement> ().SetSpriteOrientation (player.transform.position.x < transform.position.x);
			if (rb.velocity.x == 0 && Time.time - lastShotTime > timeBetweenShots) {
				Shoot ();
			}
		} else {
			filling.sprite = idleSpriteFill;
			border.sprite = idleSpriteBorder;
		}
	}

	private bool CanShoot(){
		if (Mathf.Abs (player.position.x - transform.position.x) < maxShootDistance) {
			Vector3 origin = new Vector3 (transform.position.x, transform.position.y + gunHeight, 0);
			Vector3 target = new Vector3 (player.position.x, transform.position.y + gunHeight, 0);
			RaycastHit2D[] hits = Physics2D.RaycastAll (origin, target - origin);
			foreach (RaycastHit2D h in hits) {
				if (h.collider != null) {
					if (h.collider.gameObject.tag == "Player") {					
						return true;
					}
				}
			}
		}
		return false;
	}

	private void Shoot(){
		lastShotTime = Time.time;
		bool left = player.transform.position.x < transform.position.x;
		float x = gunX;
		if (left)
			x = -gunX;
		Transform bullet = Object.Instantiate (enemyBullet, new Vector3(transform.position.x + x, transform.position.y + gunHeight, -1), new Quaternion()).transform;
		bullet.GetComponent<EnemyBullet>().Initialize(left);

		audioSource.PlayOneShot (enemySounds.GetActionClip());
	}
}
