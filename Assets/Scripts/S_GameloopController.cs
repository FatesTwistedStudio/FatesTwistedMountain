using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameloopController : MonoBehaviour
{
    public float inGameTime;
    public GameObject sceneManager;
    public GameObject player;

public void setCharacter(GameObject character)
    {
        player = character;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (sceneManager == null)
        {
            sceneManager = GameObject.FindWithTag("SceneController");
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        inGameTime= Time.time;
    }
}
