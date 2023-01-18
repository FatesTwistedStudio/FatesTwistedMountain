using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_HUD : MonoBehaviour
{

    S_EventController manager;
    public float ingameTime;

    public TMP_Text text;
    public GameObject timeParent;

    private void Awake()
    {
        manager = FindObjectOfType<S_EventController>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.currentTime < 0)
        {
            timeParent.gameObject.SetActive(true);
            ingameTime = manager.timer * 2;
            text.text = ingameTime.ToString("0:00.000");
        }
        else
        {
            timeParent.gameObject.SetActive(false);

        }


    }
}
