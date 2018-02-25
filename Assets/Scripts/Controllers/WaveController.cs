using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
	private PageController pageController;
	private EnemySpawner[] enemySpawners;

	void Start () {
		pageController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<PageController> ();

		enemySpawners = new EnemySpawner[transform.childCount];
		for (int i = 0; i < transform.childCount; ++i){
			enemySpawners [i] = transform.GetChild (i).GetComponent<EnemySpawner> ();
		}
	}

	public void NextWave(){
		bool finished = true;
		foreach (EnemySpawner es in enemySpawners) {
			if (es.RemainingWaves () > 0) {
				finished = false;
				break;
			}
		}

		if (finished) {
			pageController.NextStrip ();
		} else {
			foreach (EnemySpawner es in enemySpawners) {
				es.Spawn ();
			}
		}
	}

	public bool StillSpawning(){
		foreach (EnemySpawner es in enemySpawners) {
			if (es.StillSpawning ()) {
				return true;
			}
		}
		return false;
	}
}
