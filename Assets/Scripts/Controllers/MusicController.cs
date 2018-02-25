using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	private AudioSource audioSource;
	public AudioClip changeSceneClip;
	public AudioClip[] gameOverClip;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();		
	}
	
	public void PlayTheme(AudioClip audioClip){
		audioSource.clip = audioClip;
		audioSource.Play ();
	}

	public void PlayChangeStripClip(){
		audioSource.PlayOneShot (changeSceneClip);
	}

	public void Stop(){
		audioSource.Stop ();
	}

	public void PlayGameOverClip(){
		audioSource.Stop ();
		audioSource.PlayOneShot (gameOverClip [Random.Range (0, gameOverClip.Length)]);
	}
}
