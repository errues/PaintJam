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
    private Animator anim;

    private void Start() {
        isInvulnerable = false;
        anim = GetComponent<Animator>();
    }

    public void takeDamage(int dmg = 1) {
        getDamage(dmg);
    }

    private void Update() {
        checkDeath();
    }

    private void checkDeath() {
        if(life <= 0) {
            anim.SetTrigger("Dead");
        }
    }

    private void getDamage(int dmg) {
        life -= dmg;
        isInvulnerable = true;
        Invoke("resetInvulnerability", cdInvulnerability);
    }

    private void resetInvulnerability() {
        isInvulnerable = false;
    }
}
