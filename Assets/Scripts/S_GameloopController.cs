using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
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
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

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
        //Camera mainCamera = Instantiate(snowCam) as Camera;
        //snowCam.GetComponentInChildren<CinemachineVirtualCamera>().Follow = player.GetComponent<S_CharInfoHolder>().camFollowPoint.transform;
        //snowCam.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = player.GetComponent<S_CharInfoHolder>().camFollowPoint.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(follow!=null)
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

                //Debug.Log("" + snowCam.GetComponentInChildren<CinemachineVirtualCamera>().Follow.tag);
                //Debug.Log("" + snowCam.GetComponentInChildren<CinemachineVirtualCamera>().LookAt.tag);
                follow = GameObject.FindWithTag("FollowTarget");
                if (follow != null)
                {

                    snowCam.GetComponentInChildren<CinemachineVirtualCamera>().Follow = follow.transform;
                    snowCam.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = follow.transform;
                }
                else
                {
                    Debug.Log("No follow point on Game Controller");
                }
            }
        }

        GameObject spawner = GameObject.FindWithTag("Spawner");
        inGameTime += 1 * Time.deltaTime;
    }
}
