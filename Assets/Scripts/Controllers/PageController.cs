using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour {
	public Strip[] strips;
	public AudioClip firstClip;

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
		musicController.PlayTheme (firstClip);
	}

	private void Update(){
		if (firstFrame) {
			//strips [currentStrip].FirstWave ();
			firstFrame = false;
		}
	}			
	
	public void NextStrip(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ().Hide (); // Qué cutre!
		currentStrip++;
		if (currentStrip >= strips.Length) {
			SceneManager.LoadScene ("main");
		} else {
			cameraController.NextStrip (strips[currentStrip]);
			musicController.Stop ();
			musicController.PlayChangeStripClip ();
		}
	}

	// Para pruebas con botones
	public void PreviousStrip(){
		currentStrip = Mathf.Max (0, currentStrip - 1);
		cameraController.NextStrip (strips[currentStrip]);
	}

	public void CameraInPosition(){
		musicController.PlayTheme (strips [currentStrip].GetTheme ());
		strips [currentStrip].FirstWave ();
	}

	public void NoEnemiesRemaining(){
		if (!strips [currentStrip].StillSpawning ()) {
			strips [currentStrip].NextWave ();
		}
	}
}
