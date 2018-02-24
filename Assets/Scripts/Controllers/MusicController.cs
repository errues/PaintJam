using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource> ();		
	}
	
	public void PlayTheme(AudioClip audioClip){
		audioSource.clip = audioClip;
		audioSource.Play ();
	}
}
