using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public Maze maze;
    public GameObject player;
    public GameObject monster;
    public GameObject gui;
    public Text result;

    private static GameMaster instance;

    public static GameMaster Instance
    {
        get { return GameMaster.instance; }
    }

    void Awake() {
        if (instance)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
        }
    }

    public void MazeDone() {
        maze.InstantiateObject(player, maze.width/2, maze.height/2);
        GameObject monsterStart = maze.ExitsPlaced[Random.Range(0, 4)];
        Instantiate(monster, monsterStart.transform.position, Quaternion.identity);
    }

    public void PlayerReachedExit(GameObject player, GameObject exit)
    {
        result.text = "You're alive, for now.";
        Time.timeScale = 0;
        gui.SetActive(true);
    }

    public void PlayerDied()
    {
        result.text = "You died.";
        Time.timeScale = 0;
        gui.SetActive(true);
    }
}
