using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float transitionSpeed = 1f;
	public float followingSpeed = 1f;

	public float minDifference = 0.05f;

	private bool inTransition;

	private Vector3 goalPosition;
	private float goalSize;

	public Strip currentStrip;
	//private PageController pageController;
	private bool scroll;

	private GameObject player;
	private Camera camera;

	private void Awake(){
		inTransition = false;
		scroll = false;
	}

	private void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		camera = GetComponent<Camera> ();
		//pageController = GameObject.FindGameObjectWithTag ("PageController").GetComponent<PageController> ();

		// prueba
		NextStrip(currentStrip);
	}

	private void Update(){
		if (inTransition) {
			transform.position = Vector3.Lerp (transform.position, goalPosition, Time.deltaTime * transitionSpeed);
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, goalSize, Time.deltaTime * transitionSpeed);
			if (AlmostAtGoal()) {
				transform.position = goalPosition;
				camera.orthographicSize = goalSize;
				inTransition = false;
			}
		}

		if (scroll) {
			CalculateGoalPosition ();
			transform.position = Vector3.Lerp (transform.position, goalPosition, Time.deltaTime * followingSpeed);
		}
	}

	public void NextStrip(Strip nextStrip){
		inTransition = true;
		currentStrip = nextStrip;
		goalSize = currentStrip.GetSizeForCamera ();
		if (currentStrip.HasFixedCamera ()) {
			scroll = false;
		} else {
			scroll = true;
		}
		CalculateGoalPosition ();
	}

	private void CalculateGoalPosition(){
		if (scroll) {
			float x, y;
			goalPosition = new Vector3 (player.transform.position.x, player.transform.position.y, -5);
		} else {
			Vector3 currentStripPosition = currentStrip.GetPosition ();
			goalPosition = new Vector3 (currentStripPosition.x, currentStripPosition.y, -5);
		}
	}

	private bool AlmostAtGoal(){
		return Mathf.Abs (transform.position.x - goalPosition.x) < minDifference &&
		Mathf.Abs (transform.position.y - goalPosition.y) < minDifference &&
		Mathf.Abs (transform.position.z - goalPosition.z) < minDifference &&
		Mathf.Abs (camera.orthographicSize - goalSize) < minDifference;
	}
}
