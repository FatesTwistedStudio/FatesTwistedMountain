using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class S_OptionsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown  resDropdown;
    public Slider masterSlider, musicSlider,sfxSlider;
    public Toggle toggle;
    public AudioMixer masterVolume;
    public AudioMixer musicVolume;
    public AudioMixer sfxVolume;
    public bool FullScreen;

    Resolution[] resolutions;

    void Start()
    {

        if(PlayerPrefs.HasKey("Master_Volume")|| PlayerPrefs.HasKey("Music_Volume") || PlayerPrefs.HasKey("SFX_Volume"))
        {
            Load();
        }
        else
        {
            SetMasterVolume(masterSlider.value);
        }


        int currentResIndex = 0;

        resolutions = Screen.resolutions.refreshRate;
        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();

    }

    void Update()
    {
        /*

        */
    }

    public void SetMasterVolume(float volume)
    {
        volume = masterSlider.value;
        masterVolume.SetFloat("Master_Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master_Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        volume = musicSlider.value;
        musicVolume.SetFloat("Music_Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music_Volume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        volume = sfxSlider.value;
        sfxVolume.SetFloat("SFX_Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        FullScreen = isFullscreen;
        Screen.fullScreen = FullScreen;
        
        PlayerPrefs.SetInt("isFullscreen",FullScreen ? 1 : 0);
    }

    public void SetRes(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    private void Load()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master_Volume");
        musicSlider.value = PlayerPrefs.GetFloat("Music_Volume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_Volume");
        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        Screen.fullScreen = FullScreen;
        toggle.isOn = FullScreen;
        SetFullscreen(FullScreen);

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
    }
}