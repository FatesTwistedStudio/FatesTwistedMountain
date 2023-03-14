using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class S_SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10 (sliderValue) * 20);
    }
}
