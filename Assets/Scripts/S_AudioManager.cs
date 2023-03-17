using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class S_AudioManager : MonoBehaviour
{
    public S_Sound[] sounds;

    public static S_AudioManager instance;

    public S_BackgroundMusic bgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (S_Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.spatialBlend = s.spaitialBlend;
        }
    }

    public void Play(string name)
    {
        //To call the refrence FindObjectOfType<S_AudioManager>().Play("NameofClip");

       S_Sound s = Array.Find(sounds, sound => sound.name == name);
       
       if (s == null)
       {
            Debug.LogWarning("Sound: " + name + " not found! Check to see if you made a typo!");
            return;
       }

       if(s.source.loop ==true)
       {
            s.source.Play();
           Debug.Log("Playing Audio");
       }
       else
       {
            s.source.PlayOneShot(s.source.clip, s.volume);
           Debug.Log("Playing SFX");
          //  Debug.Log("One Shot SFX");
        }
    }

    public void StopPlaying(string name)
    {
        S_Sound s = Array.Find(sounds, item => item.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found! Check to see if you made a typo!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch / 2f, s.pitch / 2f));

        s.source.Stop();
    }
}
