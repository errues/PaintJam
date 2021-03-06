﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    //Public variables, setup in design
	public enum PlatformType { Static, Movable, Destroyable };
    public PlatformType type;
    [Header("Just for rectangular platforms")]
    public Vector2Int platformSize;
    [Header("Introduce this if Movable")]
    [Range(0.5f, 5)]
    public float speed;
    public Vector3[] positions;

    //Private variables, auto setup
    private int nextPosIndex;
    private bool loop;
    private BoxCollider2D[] cols;

    private void Start() {
        setColliders(gameObject.GetComponents<BoxCollider2D>());
        if (!type.Equals(PlatformType.Static)) {
            transform.localPosition = positions[0];
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

    private void setColliders(BoxCollider2D[] cols) {
        foreach (BoxCollider2D col in cols) {
            if (col.isTrigger) {
                Vector2 triggerSize = new Vector2(platformSize.x * 0.8f, platformSize.y * 0.05f);
                Vector2 triggerPos = new Vector2(0, platformSize.y * 0.5f);
                col.size = triggerSize;
                col.offset = triggerPos;
                transform.localPosition = positions[0];
            } else {
                col.size = platformSize;
            }
        }
    }

    private bool checkPos(Vector3 goalPos) {
        Vector3 currentPos = transform.localPosition;
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
            transform.localPosition = Vector3.Lerp(transform.localPosition, positions[nextPosIndex], speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(platformSize.x, platformSize.y, 0));
    }
}
