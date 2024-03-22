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
    public static S_GameloopController instance;
    //things to save
    public int[] highscores;

    public string SavePath;
    public void Awake()
    {
        if (SavePath != null)
        {
            LoadPlayer();
        }
        if (sceneManager == null)
        {
            sceneManager = GameObject.FindWithTag("SceneController");
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        DontDestroyOnLoad(this);

        if (instance == null) {
            instance = this;
        } else {
            DestroyObject(instance);
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
    private void OnDestroy()
    {
        Debug.Log("Death to " + gameObject.name);
        SavePlayer();
    }
    //Save and Load-------------------------------------------------------------
    //Credit: https://www.youtube.com/watch?v=XOjd_qU2Ido&t=683s
    public void SavePlayer()
    {
        S_SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        S_PlayerData data = S_SaveSystem.LoadPlayer();

        if (data != null)
        {
            highscores = data.highScores;

        }

    }

}
