using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float transitionSpeed = 1f;
	public float followingSpeed = 1f;

	public float minDifference = 0.05f;
	public float distanceToPlane = 50f;

	private bool inTransition;

	private Vector3 goalPosition;
	private float goalSize;

	private Strip currentStrip;
	private PageController pageController;
	private bool scroll;

	private GameObject player;
	private Camera myCamera;

	private void Awake(){
		inTransition = false;
		scroll = false;

		myCamera = GetComponent<Camera> ();
	}
		
	private void Start(){
		pageController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<PageController> ();
	}

	private void Update(){
		if (inTransition) {
			transform.position = Vector3.Lerp (transform.position, goalPosition, Time.deltaTime * transitionSpeed);
			myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, goalSize, Time.deltaTime * transitionSpeed);
			if (AlmostAtGoal()) {
				transform.position = goalPosition;
				myCamera.orthographicSize = goalSize;
				inTransition = false;
				pageController.CameraInPosition ();
			}
		}

		if (scroll) {
			CalculateGoalPosition ();
			transform.position = Vector3.Lerp (transform.position, goalPosition, Time.deltaTime * followingSpeed);
		}
	}

	// Es llamado en el start de pagecontroller.
	public void FirstStrip(Strip firstStrip){
		player = GameObject.FindGameObjectWithTag ("Player");
		NextStrip (firstStrip);
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
			goalPosition = new Vector3 (player.transform.position.x, player.transform.position.y, -distanceToPlane);
		} else {
			Vector3 currentStripPosition = currentStrip.GetPosition ();
			goalPosition = new Vector3 (currentStripPosition.x, currentStripPosition.y, -distanceToPlane);
		}
	}

	private bool AlmostAtGoal(){
		return Mathf.Abs (transform.position.x - goalPosition.x) < minDifference &&
		Mathf.Abs (transform.position.y - goalPosition.y) < minDifference &&
		Mathf.Abs (transform.position.z - goalPosition.z) < minDifference &&
		Mathf.Abs (myCamera.orthographicSize - goalSize) < minDifference;
	}
}
