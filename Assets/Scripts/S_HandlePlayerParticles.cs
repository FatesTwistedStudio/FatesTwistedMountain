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
    [SerializeField]
    private GameObject bigWindObj;
    [SerializeField]
    private ParticleSystem blowingSnow;

    private float something;
    public Color peakColor;
    public Color noColor;
    public Color RuntimeColor;

    private float velocity;
    private float time;

    [Header("Trail")]
    [SerializeField]
    private TrailRenderer trailRenderer;
    [SerializeField]
    private GameObject trail;
    [SerializeField]
    private Transform trailLocation;
    private GameObject modelRef;
    
    private float pauseTime;
    private float LandingTime;
    private float trailTime;


    private void Awake()
    {
        player = GetComponent<S_HoverboardPhysic>();
        rb = GetComponent<Rigidbody>();
        bigWind = bigWindObj.GetComponent<ParticleSystem>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;
        LandingTime = Mathf.Clamp(LandingTime, -3, 1);

      //  Debug.Log(velocity);
        if (player.isGrounded)
        {
            onGround();
            LandingTime -= Time.deltaTime;
        }
        else
        {
            inAir();
            LandingTime += Time.deltaTime * 2;
        }

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
        trailRenderer.time = 1;

        var emisson = bigWind.emission;
        var emColor = bigWind.main;

        emColor.startColor = new Color(1, 1, 1, 1);
        
        emisson.rateOverDistance = something;
        emisson.rateOverDistance = Mathf.Clamp(something, 0, 5);
        
        something = Mathf.Lerp(0, 0 + rb.velocity.magnitude/2, 0.2f);
        RuntimeColor.a = Mathf.Lerp(noColor.a + rb.velocity.magnitude * 0.01f, peakColor.a, 0.2f);
        RuntimeColor.a = Mathf.Clamp(RuntimeColor.a, noColor.a, peakColor.a);
        emColor.startColor = RuntimeColor;
       
        trailRenderer.time = 1;
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

        pauseTime = Time.time;
        trailRenderer.time = 0;
   
    }
 

    private void OnCollisionEnter(Collision collision)
    {
        GameObject lp = Instantiate(landingParticles, spawnpoint.transform.position, landingParticles.transform.rotation);
        var startsizelp = lp.GetComponent<ParticleSystem>().main;
        startsizelp.startSize = 0.1f;

        if (LandingTime >0)
        {
            GameObject zp = Instantiate(landingParticles, spawnpoint.transform.position, landingParticles.transform.rotation);

            var startsizezp = zp.GetComponent<ParticleSystem>().main;
            var emission = zp.GetComponent<ParticleSystem>().velocityOverLifetime;

            //emission.x = -rb.velocity.x;
            //emission.y = rb.velocity.y;
            //emission.z = -rb.velocity.z;
        }


    }
}
