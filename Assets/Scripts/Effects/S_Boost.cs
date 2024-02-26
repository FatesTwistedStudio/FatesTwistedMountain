using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Boost : MonoBehaviour
{
    private Rigidbody rb;
    private S_RefTarget target;
    private S_HandlePlayerParticles particlesRef;
    [SerializeField]
    private S_ScreenShake ssUI;
    [SerializeField]
    private float boostedSpeed;
    private float normalSpeed;
    private float speedCoolDown;
    private float speed;
    private float delay;
    private bool collided = false;

    [SerializeField]
    private GameObject[] effects;
    
     private void Update()
    {
        if (!ssUI)
        {
            ssUI = FindObjectOfType<S_ScreenShake>();   
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Player")
            {
                rb = other.GetComponent<Rigidbody>();

                ssUI.Shake();
                target = other.GetComponentInChildren<S_RefTarget>();
                particlesRef = other.GetComponent<S_HandlePlayerParticles>();
                rb.AddForce(-other.transform.right * boostedSpeed, ForceMode.VelocityChange);
                particlesRef.SpawnBurst();
                //impulse force
                FindObjectOfType<S_AudioManager>().Play("Boost");
                

            if (!collided)
            {
                var effect = Instantiate(effects[Random.Range(0, effects.Length)], target.transform.position, target.transform.rotation);
                effect.transform.parent = other.transform;
                collided = true;
            }
            //Debug.Log("Force Applied to Player");
            speed = boostedSpeed;
                StartCoroutine("SpeedDuration");
            }
       

        if (other.gameObject.tag == "Character")
        {
            //impulse force
            rb = other.GetComponent<Rigidbody>();
            rb.AddForce(-rb.transform.forward * boostedSpeed, ForceMode.VelocityChange);
            //Debug.Log("Force Applied to Character");
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
