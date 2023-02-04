using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Boost : MonoBehaviour
{
    public Vector3 boostAmount;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Force Applied");           
        if(other.gameObject.tag=="Player")
        {
            //impulse force
        }
    }
}
