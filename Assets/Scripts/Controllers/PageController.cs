using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {
	public Strip[] strips;
	private int currentStrip;

	private CameraController cameraController;

	private void Awake(){
		currentStrip = 0;
	}

	void Start () {
		cameraController = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ();
		cameraController.FirstStrip (strips[currentStrip]);
	}
	
	public void NextStrip(){		
		currentStrip = Mathf.Min (strips.Length - 1, currentStrip + 1);
		cameraController.NextStrip (strips[currentStrip]);
	}

	// Para pruebas con botones
	public void PreviousStrip(){
		currentStrip = Mathf.Max (0, currentStrip - 1);
		cameraController.NextStrip (strips[currentStrip]);
	}

	public void CameraInPosition(){

	}
}
