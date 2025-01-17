using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_NieEffect : MonoBehaviour
{
    public GameObject character;
    private GameObject unaffectedCharacter;

    public float minDuration;
    public float maxDuration;
    private float durationLeft;

    private void Start()
    {
        if (character != null)
        {
            unaffectedCharacter = character;
        }
        durationRandomizer();
    }
    private void Update()
    {
        durationLeft -=1* Time.deltaTime;
        if (durationLeft > 0)
        {
            playOneShotAudio();
            increaseAcceleration();
        }
        if (durationLeft <= 0)
        {
            destroyTheEffect();
        }
    }
    //they should play music
    private void playOneShotAudio()
    {
        Debug.Log("Catchy audio plays");
    }
    //enhance player accelleration 
    private void increaseAcceleration()
    {
        Debug.Log("Character feels themselves going faster");
    }
    //lasts for couple seconds
    private void durationRandomizer()
    {
        durationLeft = Random.Range(minDuration, maxDuration);
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
