using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class S_LevelSelect : MonoBehaviour
{
    public GameObject mainImage;
    public GameObject alternativeImage;
    public TextMeshProUGUI imageText;
    private GameObject EventController;
    private void Update()
    {
        if (EventController == null)
            EventController = GameObject.FindWithTag("EventController");
    }
    private void OnMouseOver()
    {
        alternativeImage.SetActive(true);
    }
    public void setText(string text)
    {
        imageText.text = text;
    }
    public void setImage(int image1,int image2)
    {
        mainImage.GetComponent<Image>().sprite= EventController.GetComponent<S_EventController>().levelSprites[image1];
        alternativeImage.GetComponent<Image>().sprite= EventController.GetComponent<S_EventController>().levelSprites[image2];
    }
  
    public void loadScene()
    {
        SceneManager.LoadScene(imageText.text);
    }
    public void loadScene(string sceneName)
    {

    }
}
