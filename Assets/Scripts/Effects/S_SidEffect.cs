using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SidEffect : MonoBehaviour
{

    public GameObject character;

    //shard moves foward
    public void launchForward()
    {
        Debug.Log("IceShard Moves forward");
    }

    //if character enters collider,  look at and continue foward
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != character)
        {

            if (other.gameObject.tag == "Character")
            {
                transform.LookAt(other.transform.position);
            }
            if (other.gameObject.tag == "Player")
            {
                transform.LookAt(other.transform.position);

            }
        }
    }

    //if character collides with shard
    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject != character)
        {
            //stun character
            if (collision.gameObject.tag == "Character")
            {
                Debug.Log("IceShard stuns Character");

            }
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("IceShard stuns Player");
            }
        }
    }
}
