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
    public GameObject MainBG;
    public GameObject map1;
    public GameObject map2;

    public TextMeshProUGUI imageText;
    [SerializeField]
    public Sprite[] levelSprites;
    public Sprite[] miniMapSprites;
    public string Level1,Level2,Level3;
    string currentLevel;
    public S_Transition transition;
    public Animator startlevelAnim;
    public Animator stSlAnim;

    void Start()
    {

    }
    public void OnEnable() {
        imageText.text = "";
        
    }
    

    public void OnEnter()
    {
        Debug.Log("Select1");
    }

    public void OnExit()
    {
        Debug.Log("main");
        //currentImage = mainImage;
        stSlAnim.Play("a_STSL_Start");
        stSlAnim.SetBool("Start", false);

        map1.SetActive(false);
        map2.SetActive(false);

    }

    public void setText(string text)
    {
        imageText.text = text;
    }
    public void SetLevel1()
    {
        currentLevel = Level1;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);
        map1.SetActive(true);
        map2.SetActive(false);

    }
    public void SetLevel2()
    {
        currentLevel = Level2;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);

        map1.SetActive(false);
        map2.SetActive(true);
    }
    public void SetLevel3()
    {
        currentLevel = Level3;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);

    }

    public void OnEnterLevel()
    {
        Debug.Log("Hover2");
        map2.SetActive(true);
        map1.SetActive(false);
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
            if (map1.GetComponent<Image>() != null)
            {
                map1.GetComponent<Image>().sprite = miniMapSprites[imageNum];

            }

        }

    }
    */

    public void loadScene()
    {
        transition.loadScene(currentLevel);
        //SceneManager.LoadScene(currentLevel);
    }
}
