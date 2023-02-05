using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LaserEffect : MonoBehaviour
{
    //laser defeats all projectile items
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RedFlag")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
    }
    // lasts 1 time
}
