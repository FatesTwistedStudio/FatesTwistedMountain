using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StartingLine : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {

        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        if(other.tag=="Character")
        {
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
