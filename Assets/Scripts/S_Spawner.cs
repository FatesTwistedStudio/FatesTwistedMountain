using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Spawner : MonoBehaviour
{
    public S_GameloopController S_GameloopController;
    public Camera SnowCam;
    public TextMeshProUGUI timer;
    public GameObject Spawner;
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
        spawnCharacter.AddComponent<S_PlayerInput>();
       // Debug.Log(spawnCharacter.tag);
        Camera mainCamera = Instantiate(SnowCam) as Camera;
        mainCamera.GetComponentInChildren<CinemachineVirtualCamera>().Follow = spawnCharacter.GetComponent<S_CharInfoHolder>().camFollowPoint.transform;
        mainCamera.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = spawnCharacter.GetComponent<S_CharInfoHolder>().camFollowPoint.transform;
    }
    public void Update()
    {
        //S_GameloopController.eventManager.GetComponent<S_EventController>().startText=timer;
        S_GameloopController.eventManager.GetComponent<S_EventController>().startingLine = Spawner;
    }
}
