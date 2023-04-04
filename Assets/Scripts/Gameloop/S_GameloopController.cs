using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameloopController : MonoBehaviour
{
    public GameObject player;
    public GameObject follow;
    public float inGameTime;
    public GameObject sceneManager;
    public GameObject eventManager;
    public Camera snowCam;
    public S_ItemDatabase S_ItemDatabase;
    public static S_GameloopController instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
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
    void Update()
    {
        if (follow != null)
        {
            eventManager.SetActive(true);
        }
        else
        {
            eventManager.SetActive(false);
        }
        if (player != null)
        {
            player.GetComponent<S_CharInfoHolder>().camFollowPoint.tag = "FollowTarget";
            if (snowCam != null)
            {
                follow = GameObject.FindWithTag("FollowTarget");
                if (follow != null)
                {
                    snowCam.GetComponentInChildren<CinemachineVirtualCamera>().Follow = follow.transform;
                    snowCam.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = follow.transform;
                }
                else
                {
//                    Debug.Log("No follow point on Game Controller");
                }
            }
        }
        GameObject spawner = GameObject.FindWithTag("Spawner");
        inGameTime += 1 * Time.deltaTime;
    }
}
