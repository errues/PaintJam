using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    //Public variables, setup in design
	public enum PlatformType { Static, Movable, Destroyable };
    public PlatformType type;
    [Header("Introduce this if Movable")]
    [Range(2, 5)]
    public float speed;
    public Vector3[] positions;

    //Private variables, auto setup
    private int nextPosIndex;
    private bool loop;

    private void Start() {
        if (type.Equals(PlatformType.Movable)) {
        transform.position = positions[0];
        }
        nextPosIndex = 1;
        loop = !this.CompareTag("TriggerPlatform");
    }

    private void Update() {
        if (type.Equals(PlatformType.Movable)) {
            move();
        }else if (type.Equals(PlatformType.Destroyable)) {
            // TODO
        }
    }

    private bool checkPos(Vector3 goalPos) {
        Vector3 currentPos = transform.position;
        currentPos.x = (float)Math.Round(currentPos.x, 1);
        currentPos.y = (float)Math.Round(currentPos.y, 1);
        return currentPos.Equals(goalPos);
    }

    private void move() {
        if (checkPos(positions[nextPosIndex]) && loop) {
            nextPosIndex = (nextPosIndex + 1) % positions.Length;
            if (nextPosIndex == 1) {
                loop = !this.CompareTag("TriggerPlatform");
            }
        }
        if (loop) {
            transform.position = Vector3.Lerp(transform.position, positions[nextPosIndex], speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!collision.gameObject.GetComponent<Platform>() && !type.Equals(PlatformType.Static)) {
            if (this.CompareTag("TriggerPlatform")) {
                loop = true;
            }
            collision.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        collision.gameObject.transform.SetParent(null);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.GetComponent<Platform>()) {
            Debug.Log("hola");
        }
    }
}
