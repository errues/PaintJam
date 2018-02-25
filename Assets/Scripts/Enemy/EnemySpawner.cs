using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[System.Serializable]
	public class Wave
	{
		public EnemyType[] enemies;
		public SpawnMode spawnMode;
		public float timeBetweenSpawn = 1;
		public int lifePoints = 3;
		public bool combinedColors = false;
	}

	public Wave[] waves;

	public GameObject bully;
	public GameObject jumper;
	public GameObject suicide;
	public GameObject gunned;

	public AudioClip enemySpawnClip;

	private int spawnIndex;
	private int waveIndex;

	private AudioSource audioSource;
	private EnemyController enemyController;

	private bool spawning;
	private int life;
	private bool comb;
	private EnemyType[] enemiesToSpawn;

	private void Awake(){
		spawnIndex = 0;
		waveIndex = 0;
		
		audioSource = GetComponent<AudioSource> ();
		enemyController = GameObject.FindGameObjectWithTag ("EnemyController").GetComponent<EnemyController> ();

		spawning = false;
	}

	public void Spawn(){
		if (waveIndex < waves.Length) {		
			life = waves [waveIndex].lifePoints;
			comb = waves [waveIndex].combinedColors;

			switch (waves [waveIndex].spawnMode) {
			case SpawnMode.SIMULTANEOUS:
				audioSource.PlayOneShot (enemySpawnClip);
				foreach (EnemyType et in waves[waveIndex].enemies) {
					CreateEnemy (et, life, comb);
				}
				break;
			case SpawnMode.ORDER:
				spawning = true;
				spawnIndex = 0;
				CreateEnemyArray (false);
				StartCoroutine (DelayedSpawning (waves [waveIndex].timeBetweenSpawn));
				break;
			case SpawnMode.RANDOM:
				spawning = true;
				spawnIndex = 0;
				CreateEnemyArray (true);
				StartCoroutine (DelayedSpawning (waves [waveIndex].timeBetweenSpawn));
				break;
			}
		}
		waveIndex++;
	}

	private void CreateEnemy(EnemyType type, int life, bool combined){
		GameObject enemy;
		switch (type) {
		case EnemyType.BULLY:
			enemy = Object.Instantiate (bully, transform.position, new Quaternion (), enemyController.transform); 
			enemy.GetComponent<EnemyHealth> ().Initialize (life, combined);
			break;
		case EnemyType.JUMPER:
			enemy = Object.Instantiate (jumper, transform.position, new Quaternion (), enemyController.transform); 
			enemy.GetComponent<EnemyHealth> ().Initialize (life, combined);
			break;
		case EnemyType.SUICIDE:
			enemy = Object.Instantiate (suicide, transform.position, new Quaternion (), enemyController.transform); 
			enemy.GetComponent<EnemyHealth> ().Initialize (life, combined);
			break;
		case EnemyType.GUNNED:
			enemy = Object.Instantiate (gunned, transform.position, new Quaternion (), enemyController.transform); 
			enemy.GetComponent<EnemyHealth> ().Initialize (life, combined);
			break;
		}
	}

	public int RemainingWaves(){
		return waves.Length - waveIndex;
	}

	public bool StillSpawning(){
		return spawning;
	}

	private void CreateEnemyArray(bool random){
		int[] indexes = new int[waves [waveIndex].enemies.Length];
		for (int i = 0; i < indexes.Length; ++i) {
			indexes [i] = i;
		}

		if (random) {
			for (int i = 0; i < indexes.Length - 1; ++i) {
				int j = Random.Range (i, indexes.Length);
				int aux = indexes [j];
				indexes [j] = indexes [i];
				indexes [i] = aux;
			}
		}

		enemiesToSpawn = new EnemyType[indexes.Length];
		for (int i = 0; i < indexes.Length; ++i) {
			print (indexes[i]);
			enemiesToSpawn [i] = waves [waveIndex].enemies [indexes [i]];
		}
	}

	IEnumerator DelayedSpawning(float t){
		audioSource.PlayOneShot (enemySpawnClip);
		CreateEnemy (enemiesToSpawn [spawnIndex], life, comb);
		spawnIndex++;
		if (spawnIndex >= enemiesToSpawn.Length) {
			spawning = false;
		} else {
			yield return new WaitForSeconds (t);
			StartCoroutine (DelayedSpawning (t));
		}
	}

	private void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (transform.position, 1);
	}
}
