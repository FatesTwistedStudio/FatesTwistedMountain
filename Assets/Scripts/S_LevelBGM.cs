using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LevelBGM : MonoBehaviour
{
    private S_EventController gameManager;
    public GameObject ec;

    bool playedSong = false;

    [Header("Place your BG song name here!")]
    public string BackgroundSongName;
    
    [HideInInspector]
    public S_AudioManager manager;

    void Start()
    {
        manager = FindObjectOfType<S_AudioManager>(); 
    }

    void Update()
    {
        if (ec == null)
        {
            ec = GameObject.FindWithTag("EventController");
        }
        else
        {
            if (gameManager == null)
            {
                gameManager = ec.GetComponent<S_EventController>();
            }
            else
            {
                if (gameManager.currentTime < 0 && !playedSong)
                {
                    playedSong = true;
                    manager.FadeIn(BackgroundSongName);
                }
            }
        }
    }
}
