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
        revealTransition = true;
    }

    // Update is called once per frame
    void Update()
    {

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
        StartCoroutine(TransitionDelay(.2f, buildNum));
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
