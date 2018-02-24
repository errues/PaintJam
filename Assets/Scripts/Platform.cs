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
        transform.position = positions[0];
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
            if (nextPosIndex == 0) {
                loop = loop = !this.CompareTag("TriggerPlatform");
            }
        }
        Debug.Log(nextPosIndex);
        transform.position = Vector3.Lerp(transform.position, positions[nextPosIndex], speed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (this.CompareTag("TriggerPlatform")) {
            loop = true;
            type = PlatformType.Movable;
        }
        collision.gameObject.transform.SetParent(this.gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        collision.gameObject.transform.SetParent(null);
    }
}
