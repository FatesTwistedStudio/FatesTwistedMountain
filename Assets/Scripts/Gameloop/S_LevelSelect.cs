using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class S_LevelSelect : MonoBehaviour
{
    public GameObject currentImage;
    public GameObject mainImage;
    public GameObject alternativeImage;
    public GameObject alternativeImage2;

    public TextMeshProUGUI imageText;
    [SerializeField]
    public Sprite[] levelSprites;
    public Sprite[] miniMapSprites;


    public void OnEnter()
    {
        Debug.Log("Hover1");
        alternativeImage.SetActive(true);
        mainImage.SetActive(false);
        alternativeImage2.SetActive(false);
    }

    public void OnExit()
    {
        Debug.Log("main");
        //currentImage = mainImage;
        mainImage.SetActive(true);
        alternativeImage.SetActive(false);
        alternativeImage2.SetActive(false);

    }

    public void setText(string text)
    {
        imageText.text = text;
    }

    public void OnEnterLevel()
    {
        Debug.Log("Hover2");
        alternativeImage2.SetActive(true);
        alternativeImage.SetActive(false);
        mainImage.SetActive(false);
    }

    /*
    public void setImage(int imageNum)
    {
        if (imageNum > 0)
        {
            if (mainImage.GetComponent<Image>() != null)
            {
                mainImage.GetComponent<Image>().sprite = levelSprites[imageNum];
            }
            if (alternativeImage.GetComponent<Image>() != null)
            {
                alternativeImage.GetComponent<Image>().sprite = miniMapSprites[imageNum];

            }

        }

    }
    */

    public void loadScene()
    {
        SceneManager.LoadScene(imageText.text);
    }
}
