using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioClip jumpSound;
    public AudioClip stepSound;
    public AudioClip shootSound;

    private AudioSource player;

    private void Start() {
        player = GetComponent<AudioSource>();
        player.loop = false;
    }

    public void playSteps() {
        player.PlayOneShot(stepSound);
    }

    public void playJump() {
        player.PlayOneShot(jumpSound);
    }

    public void playShoot() {
        player.PlayOneShot(shootSound);
    }

}
