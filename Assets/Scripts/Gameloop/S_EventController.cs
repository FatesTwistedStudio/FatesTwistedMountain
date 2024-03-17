using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class S_EventController : MonoBehaviour
{
    public float timer = 0;
    public float currentTime = 0f;
    public float startTime = 0.6f;
    public TextMeshProUGUI startText;
    public GameObject startingLine;
    public GameObject player;
    public bool isTimedEvent = true;
    public bool isStarted = true;
    public bool playerHasItem;
    private FTMInput _Input;
    private InputAction StartRace;

    public S_BackgroundMusic _BackgroundMusic;
    public S_Countdown countdown;
    bool playedsong = false;


    //public AudioSource bgs;
    //public AudioClip start;

    //[SerializeField]
    //public AudioClip[] bgm;

    [SerializeField]
    public GameObject[] charSpawned;

    [SerializeField]
    public float[] goldLevelTimes;
    public float[] silverLevelTimes;
    public float[] bronzeLevelTimes;
    [SerializeField]
    public float[] goldLevelPoints;
    public float[] silverLevelPoints;
    public float[] bronzeLevelPoints;

    public bool startEvent;
    bool foundPlayer;

    private void Awake()
    {
        _Input = new FTMInput();
        startEvent = false;

    }

    private void Start()
    {
        if (startingLine != null)
        {
            startingLine.SetActive(true);
        }
    }
    private void OnEnable()
    {
        currentTime = startTime;
        StartRace = _Input.StartRace.Start;
        StartRace.Enable();
        StartRace.performed += OnStartRace;
    }
    void Update()
    {
        if (!foundPlayer)
        {
            countdown = FindObjectOfType<S_Countdown>();
            player = GameObject.FindWithTag("Player");
            _BackgroundMusic = FindObjectOfType<S_BackgroundMusic>();
            foundPlayer = true;
        }

        if (player.GetComponent<S_Recovery>() == true)
        {
            player.GetComponent<S_Recovery>().hasStarted = isStarted;

        }

        if (player.GetComponent<S_HoverboardPhysic>().canMove = true)
        {
            timer += 1 * Time.deltaTime;
        }
        /*
        if (player.GetComponent<S_CharInfoHolder>().itemHeld != null)
        {
            playerHasItem = true;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                useItem(player);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                player.GetComponent<S_CharInfoHolder>().itemHeld = null;
            }
        }
        else
        {
            playerHasItem = false;
        }
        */
    }
    public void setTimedTrial(GameObject character)
    {
        currentTime = timer;
        character.GetComponent<S_CharInfoHolder>().timedTrial = currentTime;

    }
    public void playEvent()
    {
        startEvent = true;
        startingLine.SetActive(false);

        //bgs.PlayOneShot(start);
        if (currentTime >= 0)
        {
            currentTime -= 2 * Time.deltaTime;
            if (currentTime > 1)
            {
                //startText.text = currentTime.ToString("0");
            }
        }
    }
    public void useItem(GameObject character)
    {
        GameObject spawnItem = Instantiate(character.GetComponent<S_CharInfoHolder>().itemHeld, character.GetComponent<S_CharInfoHolder>().holdingPosition, transform.rotation) as GameObject;
        //  spawnItem.GetComponent<S_ItemDefine>().characterUsedItem = character;
        character.GetComponent<S_CharInfoHolder>().itemHeld = null;

    }
    public void discardItem(GameObject character)
    {
        character.GetComponent<S_CharInfoHolder>().itemHeld = null;

    }
    public void OnStartRace(InputAction.CallbackContext context)
    {
        isStarted = true;
    }
}
