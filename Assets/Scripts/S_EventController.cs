using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_EventController : MonoBehaviour
{
    public float turnSpeed;
    public GameObject player;
    [SerializeField]
    public GameObject[] charSpawner;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        charSpawner = GameObject.FindGameObjectsWithTag("Character");
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        for (int i = 0; i < charSpawner.Length; i++)
        {
            Debug.Log(charSpawner[i].name);
            charSpawner[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    public void startTheMap()
    {

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        for (int i = 0; i < charSpawner.Length; i++)
        {
            charSpawner[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playEvent();
        }
    }
    public void playEvent()
    {
        if (player != null)
        {
            if (player.GetComponent<S_CharInfoHolder>() == true)
            {
                Invoke("startTheMap", 3f);
            }
        }

    }
}
