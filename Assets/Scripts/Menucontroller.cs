using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menucontroller : MonoBehaviour {

	public void loadGame() {
        SceneManager.LoadScene("Page1");
    }

    public void exitRequest() {
        Application.Quit();
    }
}
