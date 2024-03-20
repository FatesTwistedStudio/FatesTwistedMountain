using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Boost : MonoBehaviour
{
    private CharacterController physics;
    private S_RefTarget target;
    private S_HandlePlayerParticles particlesRef;
    [SerializeField]
    private S_ScreenShake ssUI;
    [SerializeField]
    private float boostedSpeed;
    private float speed;
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
            physics = other.GetComponent<CharacterController>();

            physics.Move(-other.transform.right * boostedSpeed); //Speed Boost

            ssUI.Shake();
            target = other.GetComponentInChildren<S_RefTarget>();
            particlesRef = other.GetComponent<S_HandlePlayerParticles>();

            particlesRef.SpawnBurst();

            FindObjectOfType<S_AudioManager>().Play("Boost");

            if (!collided)
            {
                var effect = Instantiate(effects[Random.Range(0, effects.Length)], target.transform.position, target.transform.rotation);
                effect.transform.parent = other.transform;
                collided = true;
            }
        }


        if (other.gameObject.tag == "Character")
        {
            //impulse force
            physics = other.GetComponent<CharacterController>();
            physics.Move(-other.transform.right * boostedSpeed);
        }
    }
}
