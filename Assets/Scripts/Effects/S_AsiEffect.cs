using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AsiEffect : MonoBehaviour
{
    public GameObject character;
    public AudioClip airHornSound;
    private void Awake()
    {
        playSound();
        slowDownCharacter(character);
        destroyTheEffect();
    }
    //play one-shot audioclip
    public void playSound()
    {
        Debug.Log("PLay Air horn sound");
    }

    //all players lose momentum
    public void slowDownCharacter(GameObject characterNotEffected)
    {
        //find all players using eventContoller
        Debug.Log("all players slow down except "+characterNotEffected.name);
    }
    public void destroyTheEffect()
    {
        Destroy(gameObject);
    }
}
