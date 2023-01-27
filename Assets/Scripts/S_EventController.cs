using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
    public bool isStarted;
    public bool playerHasItem;


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
        if (Input.GetKeyDown(KeyCode.Q))
        {

            isStarted = true;
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
                charSpawned[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
    public void useItem(GameObject character)
    {
        GameObject spawnItem = Instantiate(character.GetComponent<S_CharInfoHolder>().itemHeld, character.GetComponent<S_CharInfoHolder>().itemHeld.GetComponent<S_ItemDefine>().holdingPosition, transform.rotation) as GameObject;
        character.GetComponent<S_CharInfoHolder>().itemHeld = null;
    
    }
}
