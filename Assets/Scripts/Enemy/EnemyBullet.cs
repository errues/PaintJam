using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	public float speed = 2f;

	private Vector3 targetDirection;

	public void SetDirection(Vector3 direction){
		targetDirection = direction;
	}

	private void Update(){
		transform.Translate (targetDirection * speed * Time.deltaTime);
	}
}
