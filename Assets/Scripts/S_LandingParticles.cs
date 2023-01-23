using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LandingParticles : MonoBehaviour
{
    [SerializeField]
    private GameObject Particles;

    [SerializeField]
    private Transform spawnpoint;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(Particles, spawnpoint.transform.position, Particles.transform.rotation);
    }
}
