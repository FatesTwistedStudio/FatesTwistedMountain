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
    public int quality;
    public Resolution res;

    private const string resolutionWidthPlayerPrefKey = "ResolutionWidth";
    private const string resolutionHeightPlayerPrefKey = "ResolutionHeight";
    private const string resolutionRefreshRatePlayerPrefKey = "RefreshRate";
    private const string fullScreenPlayerPrefKey = "FullScreen";

    Resolution[] resolutions;
    List<Resolution> filteredResolutions;
    private float currentRefreshRate;

    void Start()
    {
        Load();

        int currentResIndex = 0;

        resDropdown.ClearOptions();
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();
        filteredResolutions = new List<Resolution>();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            float aspectRatio = (float)resolutions[i].width / resolutions[i].height;

            if (resolutions[i].refreshRate == currentRefreshRate && Mathf.Approximately(aspectRatio, 16f / 9f))
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " @ " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (filteredResolutions[i].width == Screen.currentResolution.width && filteredResolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();

        toggle.onValueChanged.AddListener(SetFullscreen);
        resDropdown.onValueChanged.AddListener(SetRes);


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
        quality = qualityIndex;
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;  
        PlayerPrefs.SetInt(fullScreenPlayerPrefKey, isFullscreen ? 1 : 0);          
    }

    public void SetRes(int resolutionIndex)
    {
        res = filteredResolutions[resolutionIndex];
        PlayerPrefs.SetInt(resolutionWidthPlayerPrefKey, res.width);
        PlayerPrefs.SetInt(resolutionHeightPlayerPrefKey, res.height);
        PlayerPrefs.SetInt(resolutionRefreshRatePlayerPrefKey, res.refreshRate);

        Screen.SetResolution(
             res.width,
             res.height,
             toggle.isOn
         );
    }

    private void Load()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master_Volume");
        musicSlider.value = PlayerPrefs.GetFloat("Music_Volume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_Volume");
        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        res.width = PlayerPrefs.GetInt(resolutionWidthPlayerPrefKey, Screen.currentResolution.width);
        res.height = PlayerPrefs.GetInt(resolutionHeightPlayerPrefKey, Screen.currentResolution.height);
        res.refreshRate = PlayerPrefs.GetInt(resolutionRefreshRatePlayerPrefKey, Screen.currentResolution.refreshRate);

        toggle.isOn = PlayerPrefs.GetInt(fullScreenPlayerPrefKey, Screen.fullScreen ? 1 : 0) > 0;

        Screen.SetResolution(
             res.width,
             res.height,
             toggle.isOn
         );

    }
}
