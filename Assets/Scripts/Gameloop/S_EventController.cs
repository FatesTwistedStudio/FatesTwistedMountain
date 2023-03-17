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
    public float startTime = 5f;
    public TextMeshProUGUI startText;
    public GameObject startingLine;
    public GameObject player;
    public bool isTimedEvent = true;
    public bool isStarted = true;
    public bool playerHasItem;
    private FTMInput _Input;
    private InputAction StartRace;

    public S_BackgroundMusic _BackgroundMusic;
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

    private void Awake()
    {
        _Input = new FTMInput();
 

    }

    private void Start()
    {
        if (startingLine != null)
        {

            startingLine.SetActive(true);
        }
        if (startText != null)
        {
            startText.SetText("");

        }
        if (_BackgroundMusic != null)
        {

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
        charSpawned = GameObject.FindGameObjectsWithTag("Character");
        player = GameObject.FindWithTag("Player");
        _BackgroundMusic = FindObjectOfType<S_BackgroundMusic>();
        if (player.GetComponent<S_Recovery>() == true)
        {
            player.GetComponent<S_Recovery>().hasStarted = isStarted;

        }
        if (player.GetComponent<Rigidbody>().constraints != RigidbodyConstraints.FreezeAll)
        {
            timer += 1 * Time.deltaTime;
        }
        if (isStarted == true)
        {
            playEvent();
        }
        if (currentTime <= 1)
        {
            startingLine.SetActive(false);
            startText.SetText("Go!");
        }
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
        if (player.GetComponent<S_CharInfoHolder>().timedTrial > bronzeLevelTimes[SceneManager.GetActiveScene().buildIndex]) 
        {
            endTimedRace();
        }
    }
    public void setTimedTrial(GameObject character)
    {
        currentTime = timer;
        character.GetComponent<S_CharInfoHolder>().timedTrial = currentTime;

    }
    public void playEvent()
    {
        //bgs.PlayOneShot(start);
        if (currentTime >= 0)
        {
            if (!playedsong)
            {
                FindObjectOfType<S_AudioManager>().Play("Race-Start");

                playedsong = true;
            }
            currentTime -= 1 * Time.deltaTime;
            if (currentTime > 1)
            {

                startText.text = currentTime.ToString("0");
            }
        }
        if (currentTime <= .5f)
        {
            playAudio();
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            for (int i = 0; i < charSpawned.Length; i++)
            {
                
                charSpawned[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
            

        }
    }
    public void playAudio()
    {
        //bgs.loop = true;
        //bgs.PlayOneShot(bgm[SceneManager.GetActiveScene().buildIndex]);

    }
    public void useItem(GameObject character)
    {
        GameObject spawnItem = Instantiate(character.GetComponent<S_CharInfoHolder>().itemHeld, character.GetComponent<S_CharInfoHolder>().holdingPosition, transform.rotation) as GameObject;
        spawnItem.GetComponent<S_ItemDefine>().characterUsedItem = character;
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
    public void endTimedRace()
    {
        //not sure how to end it yet
       //. bgs.loop = false;
    }

}
