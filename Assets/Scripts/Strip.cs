using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strip : MonoBehaviour {
	[Range(1, 30)]
	public int sizeX = 1;
	[Range(1, 30)]
	public int sizeY = 1;
	[Range(0.1f, 15.0f)]
	public float cameraZoom;
	public bool fixedCamera = true;

	public Transform topBorder;
	public Transform downBorder;
	public Transform leftBorder;
	public Transform rightBorder;

	private CameraController cameraController;
	private float sizeForCamera;


	private void Awake(){
		if (fixedCamera) {
			// Calcula el tamaño que debe tener la cámara para abarcar toda la viñeta. OJO! Sólo para ratio 16:9
			sizeForCamera = Mathf.Max ((float)sizeY / 2 + 0.1f, (float)sizeX / 32 * 9 + 0.1f);
		} else {
			sizeForCamera = cameraZoom;
		}
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

	public bool HasFixedCamera(){
		return fixedCamera;
	}

	public float GetSizeForCamera(){
		return sizeForCamera;
	}

	public Vector3 GetPosition(){
		return transform.position;
	}
}
