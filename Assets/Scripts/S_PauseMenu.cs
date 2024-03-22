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
    private S_PlayerInput playerControls;
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
    public bool canPause = false;

    [SerializeField]
    private GameObject pauseUI;

    private void Awake()
    {
        playerInput = new FTMInput();
        canPause = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);

        manager = FindObjectOfType<S_EventController>();
        audioManager = FindObjectOfType<S_AudioManager>();
        playerControls = FindObjectOfType<S_PlayerInput>();
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
        if (canPause)
        {
            if (isPaused)
            {
                ActivateMenu();
            }
            else
            {
                DeactivateMenu();
            }
        }

    }

    public void ActivateMenu()
    {

        S_HoverboardPhysic playerCharacterPhysics = playerControls.gameObject.GetComponent<S_HoverboardPhysic>();

        audioManager.Play("Button-Pause");
        bool crossfinish = FindObjectOfType<S_FinishLine>().crossFinishLine;
        if(!crossfinish)
        {
            Time.timeScale = 0f;
            playerCharacterPhysics.canMove = false;
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
        S_HoverboardPhysic playerCharacterPhysics = playerControls.gameObject.GetComponent<S_HoverboardPhysic>();

        if (anim.GetBool("IsOptionEnabled") == true)
        {
            anim.SetBool("IsOptionEnabled", false);
            anim.SetBool("IsCreditsEnabled", false);
            audioManager.Play("Button-Resume");
            anim.Play("a_PM_End_OP");
            Time.timeScale = 1;
            playerCharacterPhysics.canMove = true;
            EventSystem.current.SetSelectedGameObject(null);
            isPaused = false;
        }
        else if (anim.GetBool("IsCreditsEnabled") == true)
        {
            anim.SetBool("IsCreditsEnabled", false);
            anim.SetBool("IsOptionEnabled", false);
            audioManager.Play("Button-Resume");
            anim.Play("a_PM_End_Cr");
            Time.timeScale = 1;
            playerCharacterPhysics.canMove = true;
            EventSystem.current.SetSelectedGameObject(null);
            isPaused = false;
        }
        else
        {
            audioManager.Play("Button-Resume");
            anim.Play("a_PM_End");
            Time.timeScale = 1;
            playerCharacterPhysics.canMove = true;
            //AudioListener.pause = false;
            EventSystem.current.SetSelectedGameObject(null);
            isPaused = false;
        }

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
        //load main menu
        GameObject.FindWithTag("AsyncLoader").GetComponent<ASyncLoader>().LoadLevelAsync(0);
    }

    public void EnableOptions()
    {
        if (anim.GetBool("IsOptionEnabled") == true)
        {
            anim.SetBool("IsOptionEnabled", false);
            anim.SetBool("IsControlsEnabled", false);
            anim.Play("a_PM_DisableOptions");
        }
        else
        {
            if (anim.GetBool("IsControlsEnabled") == true)
            {
                anim.Play("a_PM_DisableControls");
            }
            anim.SetBool("IsControlsEnabled", false);
            anim.SetBool("IsOptionEnabled", true);
            anim.Play("a_PM_ShowOptions");
        }
    }

    public void EnableControls()
    {
        if (anim.GetBool("IsControlsEnabled") == true)
        {

            anim.SetBool("IsOptionEnabled", false);
            anim.SetBool("IsControlsEnabled", false);
            anim.Play("a_PM_DisableControls");
        }
        else
        {
            if (anim.GetBool("IsOptionEnabled") == true)
            {
                anim.Play("a_PM_DisableControls");
            }
            anim.SetBool("IsOptionEnabled", false);
            anim.SetBool("IsControlsEnabled", true);
            anim.Play("a_PM_ShowControls");
        }
    }
}
