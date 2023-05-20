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
        Instantiate(GameManager);
        Debug.Log("gamemanager spawn");
    }
    public void setControllers()
    {
        if (GameObject.FindWithTag("GameController") == null)
        {
            spawnTheManager();

        }
        if (S_GameloopController == null)
        {
            S_GameloopController = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>();
        }
        if (pauseMenu != null)
        {
            pauseMenuControl();
        }
    }

    private void Start() 
    {

    }
    // Update is called once per frame
    void Update()
    {

        getSceneName();
        setControllers();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isCanvasActive = !isCanvasActive;
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
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void loadScene(string sceneName)
    {
        TransitionScript = FindObjectOfType<S_Transition>();
        TransitionScript.TurnOffTransition();
        StartCoroutine(TransitionDelay(1.5f, sceneName));
    }
    IEnumerator TransitionDelay(float delay,string name)
    {
        yield return new WaitForSeconds(delay);
        loadlevel(name);
    }
    public void loadlevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
