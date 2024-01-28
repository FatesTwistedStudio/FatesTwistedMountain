using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_Transition : MonoBehaviour
{
    [SerializeField]
    private Image transitionImg;
    public float transitionSpeed = 2f;
    private bool revealTransition;

    // Start is called before the first frame update
    void Start()
    {
        transitionImg = transitionImg.GetComponent<Image>();
        revealTransition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (revealTransition)
        {
            transitionImg.material.SetFloat("_CutOff", Mathf.MoveTowards(transitionImg.material.GetFloat("_CutOff"), 1.1f, transitionSpeed * Time.deltaTime));
        }
        else
        {
            transitionImg.material.SetFloat("_CutOff", Mathf.MoveTowards(transitionImg.material.GetFloat("_CutOff"), -0.1f - transitionImg.material.GetFloat("_EdgeSmoothing"), transitionSpeed * Time.deltaTime));
        }
    }

    public void TurnOnTransition()
    {
        revealTransition = true;
    }
    public void TurnOffTransition()
    {
        revealTransition = false;
    }
    public void loadScene(int buildNum)
    {
        TurnOffTransition();
        StartCoroutine(TransitionDelay(1.5f, buildNum));
    }
    IEnumerator TransitionDelay(float delay, int buildNum)
    {
        yield return new WaitForSeconds(delay);
        loadlevel(buildNum);
    }
    public void loadlevel(int buildNum)
    {
        GameObject.FindWithTag("AsyncLoader").GetComponent<ASyncLoader>().LoadLevelAsync(buildNum);
        //SceneManager.LoadScene(name);
    }

    public void sendToNextLevel(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
        FindObjectOfType<S_AudioManager>().StopPlaying("Finish-Line");
    }


}
