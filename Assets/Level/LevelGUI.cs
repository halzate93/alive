using UnityEngine;
using System.Collections;

public class LevelGUI : MonoBehaviour {

    public void Retry() {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void Menu() {
        Application.LoadLevel(0);
    }
}
