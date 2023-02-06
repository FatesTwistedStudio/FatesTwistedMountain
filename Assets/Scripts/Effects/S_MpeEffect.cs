using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MpeEffect : MonoBehaviour
{
    public GameObject icePatchEffect;
    public float duration;
    private void Update()
    {
        duration -= 1 * Time.deltaTime;
        //remains for 10 seconds,
        if (duration <= 0)
        {
            destroyTheEffect();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            slowDown(other.gameObject);
        }
        if (other.tag == "Character")
        {
            slowDown(other.gameObject);
        }
    }
    //slow player who are touching prefab
    private void slowDown(GameObject character)
    {
        Debug.Log(character.name + " is being slowed by mud");
    }
    //then turns to icepatch
    public void destroyTheEffect()
    {
        GameObject activeIcePatchEffect = Instantiate(icePatchEffect, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
    }
}
