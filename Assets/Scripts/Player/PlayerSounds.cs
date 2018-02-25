using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioClip[] jumpSound;
    public AudioClip[] stepSound;
    public AudioClip[] shootSound;
	public AudioClip[] showSound;

    private AudioSource player;

    private void Start() {
        player = GetComponent<AudioSource>();
        player.loop = false;
    }

    public void playSteps() {
        player.PlayOneShot(stepSound[Random.Range(0,stepSound.Length)]);
    }

    public void playJump() {
		player.PlayOneShot(jumpSound[Random.Range(0, jumpSound.Length)]);
    }

    public void playShoot() {
		player.PlayOneShot(shootSound[Random.Range(0, shootSound.Length)]);
    }

	public void playShow(){
		player.PlayOneShot(showSound[Random.Range(0, showSound.Length)]);
	}

}
