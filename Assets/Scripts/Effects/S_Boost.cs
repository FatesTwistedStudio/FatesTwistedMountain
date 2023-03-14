using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Boost : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float boostedSpeed;
    private float normalSpeed;
    private float speedCoolDown;
    private float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * boostedSpeed, ForceMode.VelocityChange);
            //impulse force
            Debug.Log("Force Applied to Player");
            speed = boostedSpeed;
            StartCoroutine("SpeedDuration");
        }
        if (other.gameObject.tag == "Character")
        {
            //impulse force
            rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * boostedSpeed, ForceMode.VelocityChange);
            Debug.Log("Force Applied to Character");
            speed = boostedSpeed;
            StartCoroutine("SpeedDuration");

        }
    }

    IEnumerator SpeedDuration()
    {
        yield return new WaitForSeconds(speedCoolDown);
        speed = normalSpeed;
    }
}
