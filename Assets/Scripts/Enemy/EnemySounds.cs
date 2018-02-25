using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour {
	public AudioClip[] deathClips;
	public AudioClip[] actionClips;

	public AudioClip GetDeathClip(){
		return deathClips [Random.Range (0, deathClips.Length)];
	}

	public AudioClip GetActionClip(){
		return actionClips [Random.Range (0, actionClips.Length)];
	}
}
