using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_HUD : MonoBehaviour
{

    S_EventController manager;
    public float ingameTime;
    private Rigidbody playerRB;

    public TMP_Text _timeText;
    public TMP_Text _SpeedText;
    public GameObject timeParent;
    public GameObject speedParent;

    bool foundPlayer = false;

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
        HandleTime();
        HandleSpeed();
    }

    private void HandleTime()
    {
        if (manager.currentTime < 0)
        {
          
            timeParent.gameObject.SetActive(true);
            ingameTime = manager.timer;
            _timeText.text = ingameTime.ToString("0:00.000");
        }
        else
        {
            timeParent.gameObject.SetActive(false);

        }

    }

    private void HandleSpeed()
    {
        //Debug.Log(playerRB.velocity.magnitude);

        if (manager.currentTime < 0)
        {
            if (!foundPlayer)
            {
                playerRB = FindObjectOfType<S_PlayerInput>().GetComponent<Rigidbody>();
                foundPlayer = true;
            }

            float velocity = playerRB.velocity.magnitude;

            speedParent.gameObject.SetActive(true);
            _SpeedText.text = (velocity/1.5f).ToString("0.0" + " M/S");
        }
        else
        {
            speedParent.gameObject.SetActive(false);

        }
    }
}
