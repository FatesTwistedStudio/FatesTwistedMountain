using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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


    private void Start()
    {
        startText.SetText("");
        currentTime = startTime;
        charSpawned = GameObject.FindGameObjectsWithTag("Character");
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player.GetComponent<S_Recovery>()==true)
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
            startText.SetText("Go!");
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
            if (currentTime > 1)
            {
                startText.text = currentTime.ToString("0");

            }
            currentTime -= 1 * Time.deltaTime;
        }
        if (currentTime < 0)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            for (int i = 0; i < charSpawned.Length; i++)
            {
                charSpawned[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            startingLine.SetActive(false);
        }
    }
}
