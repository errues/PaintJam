using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

	private GameObject player;

	private void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

    public void Spawn() {
		player.transform.position = transform.position;
		player.GetComponent<PlayerHealth> ().Show ();
		player.GetComponent<PlayerHealth> ().Heal ();
		player.GetComponent<PlayerSounds> ().playShow ();        
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(255, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(2, 2, 0));
    }
}
