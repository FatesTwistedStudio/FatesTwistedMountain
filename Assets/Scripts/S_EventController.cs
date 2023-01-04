using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EventController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] charSpawner;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
