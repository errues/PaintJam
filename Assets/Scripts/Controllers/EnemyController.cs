using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	private PageController pageController;

	private void Start(){
		pageController = transform.parent.GetComponent<PageController> ();
	}

	public void EnemyDied(){
		if (transform.childCount == 0) {
			pageController.NoEnemiesRemaining ();
		}
	}

	public void RemoveAllEnemies(){
		for (int i = transform.childCount - 1; i >= 0; --i) {
			Object.Destroy (transform.GetChild (i).gameObject);
		}
	}
}
