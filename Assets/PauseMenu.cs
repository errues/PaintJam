using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

    public Button ResumeButton;
    public Button ExitButton;
    public Image PauseImg;

    private bool paused;

    private void Start() {
        paused = false;
        showHideMenu();
    }

    private void Update() {
        if (Input.GetButtonDown("Pause")) {
            paused = !paused;
        }
        showHideMenu();
        if (paused) {
            Time.timeScale = 0;
            
        } else {
            Time.timeScale = 1;
        }
    }

    private void showHideMenu() {
        ResumeButton.enabled = paused;
        ResumeButton.image.enabled = paused;
        ExitButton.enabled = paused;
        ExitButton.image.enabled = paused;
        PauseImg.enabled = paused;
    }

}
