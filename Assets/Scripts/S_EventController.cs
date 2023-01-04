using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_EventController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] charSpawner;
    private void Start()
    {
        charSpawner =  GameObject.FindGameObjectsWithTag("Spawner");
        for (int i = 0; i < charSpawner.Length; i++)
        {
            charSpawner[i].SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < charSpawner.Length; i++)
            {
                charSpawner[i].SetActive(true);
            }
        }
    }
}
