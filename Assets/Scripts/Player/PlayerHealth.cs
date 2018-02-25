using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [Range(3,6)]
    public int life;
    [Range(3,5)]
    public float cdInvulnerability;

    private bool isInvulnerable;

    private void Start() {
        isInvulnerable = false;
    }

    private void Update() {
        checkDeath();
    }

    private void checkDeath() {
        if(life <= 0) {
        }
    }


    private void getDamage(int dmg) {
        life -= dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
    }
}
