using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WipEffect : MonoBehaviour
{
    public GameObject character;
    public float newGravity;
    private void Update()
    {
        slowGravity();
    }
    // gives more airtime for tricks
    public void slowGravity()
    {
        Debug.Log(character.name + " is alowing his fall");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==6)
        {
            Destroy(gameObject);
        }
    }
}
