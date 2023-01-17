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
        ingameTime = manager.timer *2;
        text.text = ingameTime.ToString("0:00.000");

    }
}
