using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_SceneController : MonoBehaviour
{
    S_Transition TransitionScript;
    public Canvas pauseMenu;
    public bool isCanvasActive;
    public S_GameloopController S_GameloopController;
    public string currentSceneName;
    public GameObject GameManager;

    public void spawnTheManager()
    {
        if (GameObject.FindWithTag("GameController") == null)
        {
            GameObject GM = Instantiate(GameManager);
            S_GameloopController = GM.GetComponent<S_GameloopController>();

        }
        else
        {
            S_GameloopController = FindObjectOfType<S_GameloopController>().GetComponent<S_GameloopController>();
        }
    }

    void Update()
    {
        getSceneName();
        if (S_GameloopController == null)
        {
            if (FindObjectOfType<S_GameloopController>() != null)
            {
                S_GameloopController = FindObjectOfType<S_GameloopController>().GetComponent<S_GameloopController>();
            }
            else
            {
                spawnTheManager();

            }
        }
        if (GameObject.FindWithTag("PauseMenu") == true)
        {
            if (pauseMenu != null)
            {
                pauseMenuControl();
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    isCanvasActive = !isCanvasActive;
                }
            }
            else
            {
                pauseMenu = GameObject.FindWithTag("PauseMenu").GetComponent<Canvas>();
            }
        }

    }
    public void pauseMenuControl()
    {
        pauseMenu.gameObject.SetActive(isCanvasActive);
        if (isCanvasActive == true)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
    public void getSceneName()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        //  debugBuildIndex();
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void loadScene(string sceneName)
    {
        TransitionScript = FindObjectOfType<S_Transition>();
        TransitionScript.TurnOffTransition();
        StartCoroutine(TransitionDelay(.2f, sceneName));
    }
    IEnumerator TransitionDelay(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        loadlevel(name);
    }
    public void loadlevel(string name)
    {
        S_GameloopController.gameObject.GetComponentInChildren<ASyncLoader>().LoadLevelAsyncWithName(name);
    }
    public void debugBuildIndex()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Debug.Log("SCENE: " + SceneManager.GetSceneByBuildIndex(i).path + " ; " + i + " ; " + SceneManager.sceneCountInBuildSettings);

        }
    }
}
