using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public PlayerMovement player;

    public void spawn() {
        if (GameObject.Find("Player(Clone)") != null) {
            Destroy(GameObject.Find("Player(Clone)"));
        }
        PlayerMovement newPlayer = Instantiate(player, this.transform);
        newPlayer.transform.SetParent(null);
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(255, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(2, 2, 0));
    }
}
