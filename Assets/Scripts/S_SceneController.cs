using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SceneController : MonoBehaviour
{
    public Canvas pauseMenu;
    public bool isCanvasActive;
    public S_GameloopController S_GameloopController;
    public string currentSceneName;
    // Start is called before the first frame update

    public void setControllers()
    {
        if (S_GameloopController == null)
        {
            S_GameloopController = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>();
        }
        if (pauseMenu != null)
        {

            pauseMenuControl();
        }
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
        SceneManager.LoadScene(sceneName);
    }
}
