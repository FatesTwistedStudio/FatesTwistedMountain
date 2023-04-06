using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BackgroundMusic : MonoBehaviour
{
    [Header("Place your BG song name here!")]
    public string BackgroundSongName;
    
    [HideInInspector]
    public S_AudioManager manager;

    void Start()
    {
        manager = FindObjectOfType<S_AudioManager>(); 
        manager.FadeIn(BackgroundSongName);
    }
}
