using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_PiiEffect : MonoBehaviour
{
    public float minDuration;
    public float maxDuration;
    private float durationLeft;

    public GameObject character;
    private GameObject GameManager;

    public Canvas splatters;


    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameController");

    }
    private void Update()
    {
        durationLeft = Time.deltaTime;
        if (durationLeft <= 0)
        {
            destroyTheEffect();
        }
        if (durationLeft > 0)
        {
            snowSplatter();
        }
    }

    // snow splatters appear
    public void snowSplatter()
    {
        colorRandomizer();
        sizeRandomizer();
    }
    //snow splaters appearance, size are randomized
    public void colorRandomizer()
    {
        Debug.Log("Random Color Picked");
    }
    public void sizeRandomizer()
    {
        Debug.Log("Random Size Picked");
    }
    // lasts a couple seconds
    private void durationRandomizer()
    {
        durationLeft = Random.Range(minDuration, maxDuration);
    }
    public void destroyTheEffect()
    {
        Destroy(gameObject);
    }
}
