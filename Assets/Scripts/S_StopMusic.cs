using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StopMusic : MonoBehaviour
{
    [HideInInspector]
    public S_AudioManager manager;
    public string[] bgAudio;
    public string backgroundSongName;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<S_AudioManager>();

    }
    public void StopAudio()
    {
        for (int i = 0; i < bgAudio.Length; i++)
        {
            manager.StopPlaying(bgAudio[i]);
        }
    }
    public void StopMusic()
    {
        manager.FadeOut(backgroundSongName);

    }
}
