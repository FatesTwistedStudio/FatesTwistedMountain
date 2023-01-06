using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class S_EventController : MonoBehaviour
{
    public float timer=0;
    public float currentTime = 0f;
    public float startTime = 5f;
    public TextMeshProUGUI startText;
    public GameObject startingLine;
    public GameObject player;
    [SerializeField]
    public GameObject[] charSpawner;
    public bool isStarted;


    private void Start()
    {
        startText.SetText("");
        currentTime = startTime;
        player = GameObject.FindWithTag("Player");
        charSpawner = GameObject.FindGameObjectsWithTag("Character");
 }

    void Update()
    {
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
            startText.SetText("Go!");
        }
    }
    public void setTimedTrial(GameObject character)
    {
        currentTime = timer;
        character.GetComponent<S_CharInfoHolder>().timedTrial=currentTime;
   
    }
    public void playEvent()
    {
        if (currentTime >= 0)
        {
            if (currentTime > 1)
            {
                startText.text = currentTime.ToString("0");

            }
            currentTime -= 1 * Time.deltaTime;
        }
        if (currentTime < 0)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            for (int i = 0; i < charSpawner.Length; i++)
            {
                charSpawner[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            startingLine.SetActive(false);
        }
    }
}
