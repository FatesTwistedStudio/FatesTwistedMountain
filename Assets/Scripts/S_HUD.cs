using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_HUD : MonoBehaviour
{
    public GameObject ec;
    public S_EventController manager;
    public float ingameTime;
    public float deltaTime;
    private Rigidbody playerRB;

    public TMP_Text _timeText;
    public TMP_Text _deltaText;
    public TMP_Text _SpeedText;
    public GameObject timeParent;
    public GameObject speedParent;
    public GameObject ItemUI;
    public Image itemHudImage;
    public TextMeshProUGUI ItemText;

    public Animator deltaAnim;



    bool foundPlayer = false;
    private void Start()
    {
       // deltaAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ec == null)
        {
            ec = GameObject.FindWithTag("EventController");

        }
        else
        {
            if (manager == null)
            {
                manager = ec.GetComponent<S_EventController>();
                manager.timer = 0;

            }
            else
            {
                HandleTime();
                HandleSpeed();
                HandleItemUI();
            }
        }
    }
    private void HandleItemUI()
    {
        ItemUI.SetActive(manager.playerHasItem);
        if (manager.playerHasItem == true)
        {
            if (playerRB.gameObject.GetComponent<S_CharInfoHolder>().itemHeld != null)
            {
                ItemText.SetText(playerRB.gameObject.GetComponent<S_CharInfoHolder>().itemHeld.name + "");

            }
            if (playerRB.gameObject.GetComponent<S_CharInfoHolder>().itemSprite != null)
            {
                itemHudImage.GetComponent<Image>().sprite = playerRB.gameObject.GetComponent<S_CharInfoHolder>().itemSprite;

            }
        }

    }
    private void HandleTime()
    {
        if (manager.currentTime < 0)
        {
            timeParent.gameObject.SetActive(true);
            ingameTime = manager.timer;
            DisplayTime(ingameTime);
        }
        else
        {
            timeParent.gameObject.SetActive(false);

        }
    }
    private void DisplayTime(float ingametime)
    {
        float minutes = Mathf.FloorToInt(ingametime / 60);
        float seconds = Mathf.FloorToInt(ingametime % 60);
        float milliSeconds = (ingametime % 1) * 1000;
        _timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    public void DisplayDeltaTime(float DeltaTime)
    {
        string sign = deltaTime >= 0 ? "+" : "-";

        if (deltaTime < 0)
        {
            deltaAnim.Play("a_TimeAdv");
        }
        else if (deltaTime == 0)
        {
            deltaAnim.Play("a_TimeEqual");
        }
        else
        {
            deltaAnim.Play("a_TimeDis");
        }


        float deltaMag = Mathf.Abs(deltaTime);
        float Dminutes = Mathf.Floor(deltaMag / 60);
        float Dseconds = Mathf.FloorToInt(deltaMag % 60);

        string deltaTimeT = Mathf.Abs(deltaMag).ToString("0:00.000");
        _deltaText.text = sign + "" + string.Format("{0:00}:{1:00}", Dminutes, Dseconds);

        Debug.Log(Dminutes);
        Debug.Log(Dseconds);
    }

    private void HandleSpeed()
    {
        if (manager.currentTime < 0)
        {
            if (!foundPlayer)
            {
                playerRB = FindObjectOfType<S_PlayerInput>().GetComponent<Rigidbody>();
                foundPlayer = true;
            }

            float velocity = playerRB.velocity.magnitude;

            speedParent.gameObject.SetActive(true);
            _SpeedText.text = (velocity / 1.5f).ToString("0.0" + " KM/H");
        }
        else
        {
            speedParent.gameObject.SetActive(false);

        }
    }
}
