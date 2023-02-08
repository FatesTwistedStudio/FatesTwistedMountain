using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AbeEffect : MonoBehaviour
{
    public GameObject character;
    public GameObject IcePatchEffect;


    public float minDuration;
    public float maxDuration;
    private float durationLeft;

    private void Update()
    {
        durationLeft = Time.deltaTime;
        if (durationLeft > 0)
        {
            increaseSpeed(character);
        }
        if (durationLeft <= 0)
        {
            destroyTheEffect();
        }
    }
    //touching the fire lows down character
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject!=character)
        {
            slowDownCharacter(other.gameObject);
        }
        if(other.gameObject.layer==6)
        {
            spawnIcePatch();
        }
    }
    public void slowDownCharacter(GameObject characterToEffect)
    {
        Debug.Log("Slowing down "+ characterToEffect.name+" due to fire");
    }
    //spawn icepatch
    private void spawnIcePatch()
    {
        GameObject activeIcePatchEffect = Instantiate(IcePatchEffect, transform.position, transform.rotation) as GameObject;
    }

    // gain speed boost
    public void increaseSpeed(GameObject character)
    {
        Debug.Log("increasing "+character.name+" speed due to fire");
    }
    //lasts a couple second
    public void destroyTheEffect()
    {
        Destroy(gameObject);
    }

}
