using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menucontroller : MonoBehaviour {

	public void loadLevel(string name) {
        SceneManager.LoadScene(name);
    }

    public void exitRequest() {
        Application.Quit();
    }
}
