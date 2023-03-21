using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StopMusic : MonoBehaviour
{
    public S_AudioManager manager;
    public string BackgroundSongName;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<S_AudioManager>();

    }
    public void StopAudio()
    {
        manager.StopPlaying(BackgroundSongName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
