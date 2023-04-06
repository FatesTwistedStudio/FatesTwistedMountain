using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class S_AudioManager : MonoBehaviour
{
    public S_Sound[] sounds;
    public static S_AudioManager instance;
    public float fadeDuration = 0.5f;
    private Coroutine _coroutine;

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

    //To call the refrence FindObjectOfType<S_AudioManager>().NameofTheFunction("NameofClip");


    public void Play(string name)
    {
       S_Sound s = Array.Find(sounds, sound => sound.name == name);
       
       if (s == null)
       {
            Debug.LogWarning("Sound: " + name + " not found! Check to see if you made a typo!");
            return;
       }
       if(s.source.loop ==true)
       {
            s.source.Play();
       }
       else
       {
            s.source.PlayOneShot(s.source.clip, s.volume);
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
        s.source.Stop();
    }

    public void Pause(string name)
    {
        S_Sound s = Array.Find(sounds, item => item.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found! Check to see if you made a typo!");
            return;
        }
        s.source.Pause();
    }
    public void UnPause(string name)
    {
        S_Sound s = Array.Find(sounds, item => item.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found! Check to see if you made a typo!");
            return;
        }
        s.source.UnPause();
    }
    
    public void FadeOut(string name)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(FadeOutCoroutine(name));
    }
    
    private IEnumerator FadeOutCoroutine(string name)
    {
        S_Sound s = Array.Find(sounds, item => item.name == name);
        
        float startVolume = s.source.volume;

        while (s.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime * fadeDuration;

            yield return null;
        }

        s.source.Stop();
        s.source.volume = startVolume;
    }

    public void FadeIn(string name)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(FadeInCoroutine(name));
    }
   
    private IEnumerator FadeInCoroutine(string name)
    {
        S_Sound s = Array.Find(sounds, item => item.name == name);

        float startVolume = 0f;
        s.source.volume = startVolume;
        s.source.Play();

        while (s.source.volume < 1)
        {
            s.source.volume = Mathf.Lerp(s.source.volume, s.volume, Time.deltaTime * fadeDuration);

            yield return null;
        }

        s.source.volume = s.volume;
    }

}
