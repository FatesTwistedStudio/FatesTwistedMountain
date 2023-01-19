using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class S_PauseMenu : MonoBehaviour
{
    public Image background;
    public TextMeshProUGUI title;
    public Button quit;
    public Button playButton;
    public S_GameloopController S_GameloopController;

    private FTMInput playerInput;
    private InputAction menu;
    
    private bool isPaused;

    [SerializeField]
    private GameObject pauseUI;

    private void Awake()
    {
        playerInput = new FTMInput();
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        S_GameloopController = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>();
        if (playButton != null)
        {
            if (S_GameloopController.player == null)
            {
                playButton.gameObject.SetActive(false);
            }
            else { playButton.gameObject.SetActive(true); }
        }
    }
    public void mysteryButton()
    {

    }
    public void endEvent()
    {

    }
    public void OnPause(InputValue value)
    {
        pauseUI.SetActive(true);
    }

    private void OnEnable()
    {
        menu = playerInput.Menu.Escape;
        menu.Enable();

        menu.performed += Pause;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseUI.SetActive(false);
        isPaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
