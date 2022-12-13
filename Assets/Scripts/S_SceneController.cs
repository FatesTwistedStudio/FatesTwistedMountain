using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class S_SceneController : MonoBehaviour
{
    public S_GameloopController S_GameloopController;
    public string currentSceneName;
    // Start is called before the first frame update
    void Start()
    {
        if(S_GameloopController == null)
        {
            S_GameloopController = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnPreCull()
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
