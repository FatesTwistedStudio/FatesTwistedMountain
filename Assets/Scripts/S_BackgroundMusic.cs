using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BackgroundMusic : MonoBehaviour
{
    [Header("Place your BG song name here!")]
    public string BackgroundSongName;
    
    [HideInInspector]
    public S_AudioManager manager;

    public void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<S_AudioManager>(); 
        manager.Play(BackgroundSongName);
    }

    // Update is called once per frame
    void Update()
    {
    

    }
}
