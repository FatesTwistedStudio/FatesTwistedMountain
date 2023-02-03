using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AiMovement : MonoBehaviour
{
    public Vector3 force;
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(force,ForceMode.Impulse );
    }
}
