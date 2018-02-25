using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strip : MonoBehaviour {
	[Range(1, 150)]
	public int sizeX = 1;
	[Range(1, 150)]
	public int sizeY = 1;
	[Range(0.1f, 75.0f)]
	public float cameraZoom = 1f;
	public bool fixedCamera = true;

	public AudioClip theme;

	public Transform topBorder;
	public Transform downBorder;
	public Transform leftBorder;
	public Transform rightBorder;

	private CameraController cameraController;
	private float sizeForCamera;

	private WaveController waveController;
	private PlayerSpawn playerSpawn;

	private void Awake(){
		CalculateSizeForCamera ();
		waveController = GetComponentInChildren<WaveController> ();
		playerSpawn = GetComponentInChildren<PlayerSpawn> ();
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

	// Necesario durante diseño, luego no
	private void CalculateSizeForCamera(){
		if (fixedCamera) {
			// Calcula el tamaño que debe tener la cámara para abarcar toda la viñeta. OJO! Sólo para ratio 16:9
			sizeForCamera = Mathf.Max ((float)sizeY / 2 + 0.1f, (float)sizeX / 32 * 9 + 0.1f);
		} else {
			sizeForCamera = cameraZoom;
		}
	}

	public float GetSizeForCamera(){
		CalculateSizeForCamera ();
		return sizeForCamera;
	}

	public Vector3 GetPosition(){
		return transform.position;
	}

	public AudioClip GetTheme(){
		return theme;
	}

	public void FirstWave(){
		playerSpawn.Spawn ();
		NextWave ();
	}

	public void NextWave(){
		waveController.NextWave ();
	}

	public bool StillSpawning(){
		return waveController.StillSpawning ();
	}
}
