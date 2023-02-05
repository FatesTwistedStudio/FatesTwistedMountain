using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BfgEffect : MonoBehaviour
{

    public GameObject character;
    private GameObject unaffectedCharacter;
    private GameObject GameManager;

    public float minDuration;
    public float maxDuration;
    private float durationLeft;

    public Material effectMat;

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameController");
        if (character != null)
        {
            unaffectedCharacter = character;
        }
        durationRandomizer();
    }
    private void Update()
    {
        if (durationLeft > 0)
        {
            changeColor();
            increaseSpeed();
            noEffects();
        }
        durationLeft = Time.deltaTime;
        if (durationLeft <= 0)
        {
            destroyTheEffect();
        }
    }
    private void durationRandomizer()
    {
        durationLeft = Random.Range(minDuration, maxDuration);
    }
    private void changeColor()
    {
        Debug.Log("change player color");
    }
    //ignore all effects
    private void noEffects()
    {
        Debug.Log("ignore all effects");
    }
    //speed up character
    private void increaseSpeed()
    {
        Debug.Log("speed up character");
    }
    public void destroyTheEffect()
    {
        Destroy(gameObject);
    }
    //for random amount of time
    private void OnDestroy()
    {
        character = unaffectedCharacter;
    }
}
