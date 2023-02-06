using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SfbEffect : MonoBehaviour
{
    public GameObject icePatchEffect;
    private void OnCollisionEnter(Collision collision)
    {
        //icepatch appears whereever fire touches grund
        if (collision.gameObject.layer == 6)
        {
            GameObject activeIcePatchEffect = Instantiate(icePatchEffect, transform.position, transform.rotation) as GameObject;

        }
        //players who touch fire are stunned
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player just got stunned by a fireball");
        }
        if (collision.gameObject.tag == "Character")
        {
            Debug.Log("Character just got stunned by a fireball");

        }
    }
}
