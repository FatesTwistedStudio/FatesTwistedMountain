using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameloopController : MonoBehaviour
{
    public GameObject player;
    public float inGameTime;
    public GameObject sceneManager;
    public Camera snowCam;
    public void SetCharacter(GameObject character)
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
        Camera mainCamera = Instantiate(snowCam) as Camera;
        snowCam.GetComponentInChildren<CinemachineVirtualCamera>().Follow = player.GetComponent<S_CharInfoHolder>().camFollowPoint.transform;
        snowCam.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = player.GetComponent<S_CharInfoHolder>().camFollowPoint.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (snowCam != null)
            {
                snowCam.GetComponentInChildren<CinemachineVirtualCamera>().Follow = player.GetComponent<S_CharInfoHolder>().camFollowPoint.transform;
                snowCam.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = player.GetComponent<S_CharInfoHolder>().camFollowPoint.transform;

            }
        }

        GameObject spawner = GameObject.FindWithTag("Spawner");
        inGameTime += 1 * Time.deltaTime;
    }
}
