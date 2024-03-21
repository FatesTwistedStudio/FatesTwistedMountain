using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_Countdown : MonoBehaviour
{
    public TMP_Text text;
    private S_HoverboardPhysic player;
    public bool goBegan = false;
    public S_EventController controller;
    public S_PauseMenu pauseMenu;
    public void Start3()
    {
        FindObjectOfType<S_AudioManager>().Play("Race-Countdown");
        text.text = "3";
        pauseMenu.canPause = false;
    }
    public void Start2()
    {
        FindObjectOfType<S_AudioManager>().Play("Race-Countdown");
        text.text = "2";
    }
    public void Start1()
    {
        FindObjectOfType<S_AudioManager>().Play("Race-Countdown");
        text.text = "1";
    }
    public void GO()
    {
        goBegan = true;
        FindObjectOfType<S_AudioManager>().Play("Race-Start");
        text.text = "GO!";
        pauseMenu.canPause = true;
        player = FindObjectOfType<S_HoverboardPhysic>();
        player.GetComponent<S_HoverboardPhysic>().canMove = true;

    }


    // Start is called before the first frame update
    void Start()
    {
        goBegan = false;
    }

    // Update is called once per frame
    void Update()
    {
        controller = FindObjectOfType<S_EventController>();
        if (goBegan)
        {
        controller.playEvent();

        }

    }
}
