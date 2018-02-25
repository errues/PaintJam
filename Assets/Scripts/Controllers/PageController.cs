using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {
	public Strip[] strips;
	private int currentStrip;

	private CameraController cameraController;
	private MusicController musicController;

	private bool firstFrame;

	private void Awake(){
		currentStrip = 0;
		firstFrame = true;
	}

	void Start () {
		musicController = GetComponent<MusicController> ();

		cameraController = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ();
		cameraController.FirstStrip (strips[currentStrip]);
		musicController.PlayTheme (strips [currentStrip].GetTheme ());
	}

	private void Update(){
		if (firstFrame) {
			strips [currentStrip].NextWave ();
			firstFrame = false;
		}
	}			
	
	public void NextStrip(){
		currentStrip++;
		if (currentStrip >= strips.Length) {
			//finish
		} else {
			cameraController.NextStrip (strips[currentStrip]);
			musicController.Stop ();
			musicController.PlayChangeStripClip ();
			strips [currentStrip].NextWave ();
		}
	}

	// Para pruebas con botones
	public void PreviousStrip(){
		currentStrip = Mathf.Max (0, currentStrip - 1);
		cameraController.NextStrip (strips[currentStrip]);
	}

	public void PlayerDissapeared(){

	}

	public void CameraInPosition(){
		musicController.PlayTheme (strips [currentStrip].GetTheme ());
	}

	public void NoEnemiesRemaining(){
		if (!strips [currentStrip].StillSpawning ()) {
			strips [currentStrip].NextWave ();
		}
	}
}
