using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HandlePlayerParticles : MonoBehaviour
{
    [SerializeField]
    private Transform orientation;
    private S_HoverboardPhysic player;
    private Rigidbody rb;

    [Header("Landing Particles")]
    [SerializeField]
    private GameObject smallLandingParticles;
    [SerializeField]
    private GameObject dynamicLandingParticles;
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

    [Header("Ground Wind Effect")]
    [SerializeField]
    private ParticleSystem smallWind;
    [SerializeField]
    private ParticleSystem bigWind;
    [SerializeField]
    private GameObject bigWindObj;
    [SerializeField]
    private ParticleSystem blowingSnow;

    [Header("Air Wind Effect")]
    [SerializeField]
    private ParticleSystem airSmallWind;
    [SerializeField]
    private ParticleSystem airBigWind;

    [Header("Burst Wind Effect")]
    [SerializeField]
    private GameObject BurstParticles;
    [SerializeField]
    private Transform spawnpoint2;


    private float something;
    public Color peakColor;
    public Color noColor;
    public Color RuntimeColor;
    public Color AirRuntimeColor;
    private float velocity;
    private float time;

    [Header("Trail")]
    [SerializeField]
    private TrailRenderer trailRenderer;
    [SerializeField]
    private TrailRenderer windTrailRendererFront;
    [SerializeField]
    private TrailRenderer windTrailRendererBack;
    [SerializeField]
    private GameObject trailprefab;
    private GameObject newTrail;
    [SerializeField]
    private Transform trailLocation;
    private GameObject modelRef;
    
    private float pauseTime;
    private float LandingTime;
    private float trailTime;

    private bool spawnedTrail;


    private void Awake()
    {
        player = GetComponent<S_HoverboardPhysic>();
        rb = GetComponent<Rigidbody>();
        bigWind = bigWindObj.GetComponent<ParticleSystem>();

    }

    void Update()
    {
        velocity = rb.velocity.magnitude;
        LandingTime = Mathf.Clamp(LandingTime, -3, 1);

      //  Debug.Log(velocity);
        if (player.isGrounded)
        {
            onGround();
            LandingTime = -1;
            if(!spawnedTrail)
            {
                newTrail = Instantiate(trailprefab, trailLocation.transform.position, trailLocation.transform.rotation);
                newTrail.transform.SetParent(orientation.transform);
                
                spawnedTrail = true;
            }
        }
        else
        {
            inAir();
            if (newTrail == null)
            {
                return;
            }
            else
            {
                newTrail.transform.parent = null;
                spawnedTrail = false;

            }
            
            LandingTime += Time.deltaTime;
        }
       // Debug.LogWarning(LandingTime);

    }

    private void onGround()
    {
        snowstreamL.Play();
        snowstreamR.Play();
        snowboardSnowR.Play();
        snowboardSnowL.Play();
        smallWind.Play();
        bigWind.Play();
        blowingSnow.Play();

        airSmallWind.Pause();
        airBigWind.Pause();
        
        var emisson = bigWind.emission;
        var emColor = bigWind.main;
        var emissionColor = airBigWind.main;
        var smolemissionColor = smallWind.main;
        
        emisson.rateOverDistance = something;
        emisson.rateOverDistance = Mathf.Clamp(something, 0, 5);
        
        something = Mathf.Lerp(0, 0 + rb.velocity.magnitude/2, 0.2f);
        RuntimeColor.a = Mathf.Lerp(noColor.a + rb.velocity.magnitude * 0.01f, peakColor.a, 0.2f);
        RuntimeColor.a = Mathf.Clamp(RuntimeColor.a, noColor.a, peakColor.a);
        
        emColor.startColor = RuntimeColor;
        emissionColor.startColor = noColor;
        smolemissionColor.startColor = noColor;

        //trailRenderer.time = 1;
        windTrailRendererFront.time = 0;
        windTrailRendererBack.time = 0;
    }

    private void inAir()
    {
        snowstreamR.Pause();
        snowstreamL.Pause();
        snowboardSnowR.Pause();
        snowboardSnowL.Pause();
        smallWind.Pause();
        bigWind.Pause();
        blowingSnow.Pause();

        airSmallWind.Play();
        airBigWind.Play();
        var emissionColor = airBigWind.main;
        var smolemissionColor = airSmallWind.main;

        AirRuntimeColor.a = Mathf.Lerp(noColor.a + LandingTime/2, peakColor.a, 0.01f);
        AirRuntimeColor.a = Mathf.Clamp(AirRuntimeColor.a, noColor.a, peakColor.a);

        emissionColor.startColor = AirRuntimeColor;
        smolemissionColor.startColor = AirRuntimeColor;

        pauseTime = Time.time;
        //trailRenderer.time = 0;
        windTrailRendererFront.time = 1;
        windTrailRendererBack.time = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject lp = Instantiate(smallLandingParticles, spawnpoint.transform.position, smallLandingParticles.transform.rotation);
        var startsizelp = lp.GetComponent<ParticleSystem>().main;
        startsizelp.startSize = 0.1f;

        

        if (LandingTime >0)
        {
            GameObject zp = Instantiate(dynamicLandingParticles, spawnpoint.transform.position, smallLandingParticles.transform.rotation);

            var startsizezp = zp.GetComponent<ParticleSystem>().main;
            var emission = zp.GetComponent<ParticleSystem>().velocityOverLifetime; 
            FindObjectOfType<S_AudioManager>().Play("Snow-Landing");
            startsizezp.startSize = rb.velocity.magnitude * 0.01f;
            startsizezp.startSpeed = rb.velocity.magnitude * 0.4f;
            //Debug.LogWarning("Big landing, particles spawned");
        }
        
    }

    public void SpawnBurst()
    {
        Instantiate(BurstParticles, spawnpoint2.transform.position, spawnpoint2.transform.rotation);
    }
}
