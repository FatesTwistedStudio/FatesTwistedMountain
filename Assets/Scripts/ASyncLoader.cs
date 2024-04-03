//Credit: https://www.youtube.com/watch?v=NyFYNsC3H8k - https://www.youtube.com/watch?v=YMj2qPq9CP8
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Unity.Mathematics;

public class ASyncLoader : MonoBehaviour
{
    //[Header("Menu Screens")]
    //[SerializeField] private GameObject loadingScreen;
    //[]
    public GameObject loadScreen;
    public Slider slider;

    public Image Background;
    public Sprite[] BackgroundImages;

    public TextMeshProUGUI LoadScreenHeaderText;
    public string[] headers;
    public TextMeshProUGUI LoadScreenLevelDetailText;
    public string[] levelDetails;
    public TextMeshProUGUI LoadScreenLoadingDetailsText;
    public string[] loadingDetails;
    public TextMeshProUGUI LoadScreenPercentage;

    float sliderProgress;
    float elapsedTime;
    float fakeLoadTime = 0.5f;

    public void LoadLevelAsync(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    public void LoadLevelAsyncWithName(string name)
    {
        if (name != "MainMenu")
        {
            //Debug.Log(name);
            int index = SceneManager.GetSceneByName(name).buildIndex;
            //Debug.Log(index);
            if (index <= -1)
            {
                Debug.LogError("The Build Index for " + name + " has returned " + index);
            }
            else
            {

                LoadLevelAsync(index);
            }
        }
        else
        {
            LoadLevelAsync(0);
        }
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        sliderProgress = 0f;
        elapsedTime = 0f;
        //create operation for loading async
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        //turn on the canvas
        loadScreen.SetActive(true);
        //set canvas while loading
        //Debug.Log("Progress is:" + (operation.progress * 100) + "%");

        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress / .9f);
            float progress = Mathf.Clamp01(elapsedTime / fakeLoadTime);
            slider.value = progress;
            //slider.value = Mathf.Lerp(0f, 1f, progress);
            setLoadingScreenDetails(sceneIndex, progress);
            elapsedTime += Time.deltaTime;
            //Debug.Log("Elasped time is " + elapsedTime);
            yield return null;
        }
       // Debug.Log("Elasped time is " + elapsedTime);
        //turn off loading canvas when done
        if (operation.isDone)
        {
            loadScreen.SetActive(false);
        }
    }
    public void setLoadingScreenDetails(int sceneIndex, float progress)
    {
        string formattedPercentage = (progress * 100f).ToString("F0") + "%";
        //slider.value = progress;
        LoadScreenPercentage.SetText(formattedPercentage);

        Background.sprite = BackgroundImages[sceneIndex];
        LoadScreenHeaderText.SetText(headers[sceneIndex]);
        LoadScreenLevelDetailText.SetText("Level:" + levelDetails[sceneIndex]);

        // int r = Random.Range(0, levelDetails.Length);
        LoadScreenLoadingDetailsText.SetText(loadingDetails[0]);

    }
    void OnEnable()
    {

    }
}
