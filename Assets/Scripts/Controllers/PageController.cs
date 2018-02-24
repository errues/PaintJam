using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {
	public Strip[] strips;
	private int currentStrip;

	private CameraController cameraController;
	private MusicController musicController;

	private void Awake(){
		currentStrip = 0;
	}

	void Start () {
		musicController = GetComponent<MusicController> ();

		cameraController = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ();
		cameraController.FirstStrip (strips[currentStrip]);
		musicController.PlayTheme (strips [currentStrip].GetTheme ());
	}
	
	public void NextStrip(){		
		currentStrip = Mathf.Min (strips.Length - 1, currentStrip + 1);
		cameraController.NextStrip (strips[currentStrip]);
		musicController.PlayTheme (strips [currentStrip].GetTheme ());
	}

	// Para pruebas con botones
	public void PreviousStrip(){
		currentStrip = Mathf.Max (0, currentStrip - 1);
		cameraController.NextStrip (strips[currentStrip]);
	}

	public void CameraInPosition(){

	}
}
