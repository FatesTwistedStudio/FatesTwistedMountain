using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class S_EventController : MonoBehaviour
{
    public float timer = 0;
    public float currentTime = 0f;
    public float startTime = 5f;
    public TextMeshProUGUI startText;
    public GameObject startingLine;
    public GameObject player;
    [SerializeField]
    public GameObject[] charSpawned;
    public bool isStarted = true;
    public bool playerHasItem;
    private FTMInput _Input;
    private InputAction StartRace;

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
    }
    public void setTimedTrial(GameObject character)
    {
        currentTime = timer;
        character.GetComponent<S_CharInfoHolder>().timedTrial = currentTime;

    }
    public void playEvent()
    {
        if (currentTime >= 0)
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime > 1)
            {
                startText.text = currentTime.ToString("0");

            }
        }
        if (currentTime <= .5f)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            for (int i = 0; i < charSpawned.Length; i++)
            {
                charSpawned[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
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

    
}
