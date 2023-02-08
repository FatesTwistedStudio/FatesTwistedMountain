using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_IcePatchEffect : MonoBehaviour
{
    private float durationLeft;

    private void Update()
    {
        durationLeft = Time.deltaTime;
 
        if (durationLeft <= 0)
        {
            destroyTheEffect();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            slowDownControl(other.gameObject);
            slowDownSpeed(other.gameObject);
        }
        if (other.tag == "Player")
        {
            //slow down player input while on patch
            Debug.Log("player is having trouble on the ice");
            slowDownControl(other.gameObject);
            //speed up player
            Debug.Log("player is speeding up on the ice");
            slowDownSpeed(other.gameObject);
        }
    }
    private void slowDownSpeed(GameObject character)
    {
        //slow down AI input while on patch
        Debug.Log("AI is having trouble on the ice");
    }
    private void slowDownControl(GameObject character)
    {
        //speed up AI
        Debug.Log("AI is speeding up on the ice");
    }
    public void destroyTheEffect()
    {
        Destroy(gameObject);
    }

}
