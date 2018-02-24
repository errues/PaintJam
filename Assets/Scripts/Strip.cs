using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strip : MonoBehaviour {
	[Range(1, 30)]
	public int sizeX = 1;
	[Range(1, 30)]
	public int sizeY = 1;

	public bool fixedCamera = true;
	[Range(0.0f, 10.0f)]
	public float cameraZoom;

	private CameraController cameraController;

	public Transform topBorder;
	public Transform downBorder;
	public Transform leftBorder;
	public Transform rightBorder;

	private void Start(){

	}

	private void OnValidate(){
		topBorder.localScale = new Vector3 (sizeX + 0.1f, 0.1f, 1);
		downBorder.localScale = new Vector3 (sizeX + 0.1f, 0.1f, 1);
		leftBorder.localScale = new Vector3 (sizeY + 0.1f, 0.1f, 1);
		rightBorder.localScale = new Vector3 (sizeY + 0.1f, 0.1f, 1);

		topBorder.localPosition = new Vector3 (0, (sizeY - 1) * 0.5f + 0.5f, 0);
		downBorder.localPosition = new Vector3 (0, (sizeY - 1) * -0.5f - 0.5f, 0);
		leftBorder.localPosition = new Vector3 ((sizeX - 1) * -0.5f - 0.5f, 0, 0);
		rightBorder.localPosition = new Vector3 ((sizeX - 1) * 0.5f + 0.5f, 0, 0);
	}

	public void Activate(){
		
	}		
}
