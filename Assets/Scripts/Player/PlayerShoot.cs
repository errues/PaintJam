using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
	public float timeBetweenShots = 0.1f;
	public float gunHeight = 0f;
	public float gunX = 0f;

	public int regularDamage = 1;

	public GameObject bullet;

	private float timeSinceLastShot;
	private bool bluePressed, yellowPressed, redPressed;

	private PlayerMovement playerMovement;
	private PlayerSounds playerSounds;
    private Animator anim;

	private void Awake(){
		bluePressed = yellowPressed = redPressed = false;
		timeSinceLastShot = Time.time;

		playerMovement = GetComponent<PlayerMovement> ();
		playerSounds = GetComponent<PlayerSounds> ();
        anim = GetComponent<Animator>();
	}

	private void Update(){
		if (Input.GetAxis ("Fire_Blue") == 0) {
			bluePressed = false;
		} else if (Time.time - timeSinceLastShot > timeBetweenShots && !bluePressed){
			bluePressed = true;
			Shoot (BasicColors.AZUL);
		}

		if (Input.GetAxis ("Fire_Yellow") == 0) {
			yellowPressed = false;
		} else if (Time.time - timeSinceLastShot > timeBetweenShots && !yellowPressed){
            yellowPressed = true;
			Shoot (BasicColors.AMARILLO);
		}

		if (Input.GetAxis ("Fire_Red") == 0) {
			redPressed = false;
		} else if (Time.time - timeSinceLastShot > timeBetweenShots && !redPressed){
            redPressed = true;
			float a = Time.time - timeSinceLastShot;
			Shoot (BasicColors.ROJO);
		}
	}

	private void Shoot(BasicColors color){
		timeSinceLastShot = Time.time;
		float x = gunX;
		bool left = !playerMovement.GetFacing ();
		if (left) {
			x = -gunX;
		}
		playerSounds.playShoot ();
		Transform bull = Object.Instantiate (bullet, new Vector3 (transform.position.x + x, transform.position.y + gunHeight, -1), new Quaternion ()).transform;
		bull.GetComponent<Bullet> ().Initialize (color, regularDamage, left);
	}
}
