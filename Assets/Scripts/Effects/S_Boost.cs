using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Boost : MonoBehaviour
{
    public Vector3 boostAmount;
    public float normalSpeed;
    public float boostedSpeed;
    public float speedCoolDown;
    public float speed;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //impulse force
            Debug.Log("Force Applied to Player");
            speed = boostedSpeed;
            StartCoroutine("SpeedDuration");
        }
        if (other.gameObject.tag == "Character")
        {
            //impulse force
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
