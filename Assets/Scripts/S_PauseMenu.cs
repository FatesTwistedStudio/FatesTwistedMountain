using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class S_PauseMenu : MonoBehaviour
{
    public Image background;
    public TextMeshProUGUI title;
    public Button quit;
    public Button playButton;
    public S_GameloopController S_GameloopController;
    public S_EventController manager;
    private FTMInput playerInput;
    private InputAction menu;
    public GameObject resumeButton;
    public GameObject HUD;
    public Animator anim;
    public S_Transition transiton;
    [HideInInspector]
    public S_AudioManager audioManager;
    S_StopMusic endAudio;
    
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

        manager = FindObjectOfType<S_EventController>();
        audioManager = FindObjectOfType<S_AudioManager>(); 
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
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton);

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
        audioManager.Play("Button-Pause");
        bool crossfinish = FindObjectOfType<S_FinishLine>().crossFinishLine;
        if(!crossfinish)
        {
            Time.timeScale = Time.unscaledDeltaTime * 0.3f;
            //AudioListener.pause = true;
            anim.Play("a_PM_Start");
            HUD.SetActive(false);
            pauseUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
      Debug.Log("crossfinish" + crossfinish);
    }

    public void DeactivateMenu()
    {
        audioManager.Play("Button-Resume");
        anim.Play("a_PM_End");
        Time.timeScale = 1;
        //AudioListener.pause = false;
        EventSystem.current.SetSelectedGameObject(null);
        isPaused = false;
    }
    public void EndUI()
    {
        pauseUI.SetActive(false);
        HUD.SetActive(true);
    }

    public void Quit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        endAudio = FindObjectOfType<S_StopMusic>();
        endAudio.StopAudio();
        endAudio.StopMusic();

        Time.timeScale = 1;
        AudioListener.pause = false;
        transiton.loadScene("MainMenu");
        //SceneManager.LoadScene(0);
    }
}
