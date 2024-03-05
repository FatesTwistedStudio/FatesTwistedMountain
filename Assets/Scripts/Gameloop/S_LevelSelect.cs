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
    public string Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8;
    string currentLevel;
    public int buildIndex;
    public S_Transition transition;
    public Animator startlevelAnim;
    public Animator stSlAnim;

    void Start()
    {

    }
    public void loadScene()
    {
        transition.loadScene(buildIndex);
    }

    public void OnEnable()
    {
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
        buildIndex = 2;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);
        map1.SetActive(true);
        map2.SetActive(false);

    }
    public void SetLevel2()
    {
        currentLevel = Level2;
        buildIndex = 3;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);

        map1.SetActive(false);
        map2.SetActive(true);
    }
    public void SetLevel3()
    {
        currentLevel = Level3;
        buildIndex = 4;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);

    }
    public void SetLevel4()
    {
        currentLevel = Level4;
        buildIndex = 5;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);

    }
    public void SetLevel5()
    {
        currentLevel = Level5;
        buildIndex = 6;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);

    }
    public void SetLevel6()
    {
        currentLevel = Level6;
        buildIndex = 7;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);

    }
    public void SetTestLevel(int Index)
    {
        currentLevel = Level3;
        buildIndex = Index;
        startlevelAnim.SetBool("Entry", true);
        stSlAnim.SetBool("Start", true);
    }
    public void OnEnterLevel()
    {
        Debug.Log("Hover2");
        map2.SetActive(true);
        map1.SetActive(false);
    }


}
