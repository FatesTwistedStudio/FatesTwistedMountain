using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Spawner : MonoBehaviour
{
    public S_GameloopController S_GameloopController;
    public Camera snowCam;

    // Start is called before the first frame update
    void Start()
    {
        if (S_GameloopController == null)
        {
            S_GameloopController = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>();
        }
        GameObject spawnCharacter = Instantiate(S_GameloopController.player, transform.position, transform.rotation) as GameObject;
        spawnCharacter.gameObject.GetComponent<PlayerInput>().enabled = true;
        spawnCharacter.tag = "Player";
        // Debug.Log(spawnCharacter.tag);  Camera mainCamera = Instantiate(snowCam) as Camera;
       
    }

}
