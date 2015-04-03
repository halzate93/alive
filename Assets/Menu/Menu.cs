using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public string levelName;

    public void Play() {
        Application.LoadLevel(levelName);
    }

    public void Quit() {
        Application.Quit();
    }
}
