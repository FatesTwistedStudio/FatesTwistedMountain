using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_IcePatchEffect : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            //slow down AI input while on patch
            Debug.Log("AI is having trouble on the ice");
            //speed up AI
            Debug.Log("AI is speeding up on the ice");

        }
        if (other.tag == "Player")
        {
            //slow down player input while on patch
            Debug.Log("player is having trouble on the ice");
            //speed up player
            Debug.Log("player is speeding up on the ice");
        }
    }



}
