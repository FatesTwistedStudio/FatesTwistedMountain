using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class S_Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float spaitialBlend;
    
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;


}
