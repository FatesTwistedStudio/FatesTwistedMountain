using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HandlePlayerParticles : MonoBehaviour
{
    private S_HoverboardPhysic player;
    private Rigidbody rb;

    [Header("Landing Particles")]
    [SerializeField]
    private GameObject landingParticles;
    [SerializeField]
    private Transform spawnpoint;

    [Header("Snow Stream")]
    [SerializeField]
    private ParticleSystem snowstreamR;
    [SerializeField]
    private ParticleSystem snowstreamL;

    [Header("Snow from Snowboard")]
    [SerializeField]
    private ParticleSystem snowboardSnowR;
    [SerializeField]
    private ParticleSystem snowboardSnowL;

    [Header("Wind Effect")]
    [SerializeField]
    private ParticleSystem smallWind;
    [SerializeField]
    private ParticleSystem bigWind;
    private float minStartSpeed, maxStartSpeed;

    private float velocity;
    private float time;

    private void Awake()
    {
        player = GetComponent<S_HoverboardPhysic>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;

      //  Debug.Log(velocity);
        if (player.isGrounded)
        {
            onGround();
        }
        else
        {
            inAir();
        }
    }

    private void onGround()
    {
        snowstreamL.Play();
        snowstreamR.Play();
        snowboardSnowR.Play();
        snowboardSnowL.Play();
        smallWind.Play();

    }

    private void inAir()
    {
        snowstreamR.Pause();
        snowstreamL.Pause();
        snowboardSnowR.Pause();
        snowboardSnowL.Pause();
        smallWind.Pause();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(landingParticles, spawnpoint.transform.position, landingParticles.transform.rotation);
    }
}
