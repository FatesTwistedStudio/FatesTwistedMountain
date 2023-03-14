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
    public GameObject mainImage;
    public GameObject alternativeImage;
    public TextMeshProUGUI imageText;
    [SerializeField]
    public Sprite[] levelSprites;
    public Sprite[] miniMapSprites;

    private void OnMouseOver()
    {
        alternativeImage.SetActive(true);
    }
    public void setText(string text)
    {
        imageText.text = text;
    }
    public void setImage(int imageNum)
    {
        if (imageNum > 0)
        {
            if (mainImage.GetComponent<Image>() != null)
            {
                Debug.Log("main");
                mainImage.GetComponent<Image>().sprite = levelSprites[imageNum];
            }
            if (alternativeImage.GetComponent<Image>() != null)
            {
                alternativeImage.GetComponent<Image>().sprite = miniMapSprites[imageNum];

            }

        }

    }

    public void loadScene()
    {
        SceneManager.LoadScene(imageText.text);
    }
}
