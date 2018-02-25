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
        InvokeRepeating("playSingleStep", 0.2f, 1f);
    }

    public void stopPlay() {
        CancelInvoke();
    }

    private void playSingleStep() {
        player.PlayOneShot(stepSound);
    }

}
