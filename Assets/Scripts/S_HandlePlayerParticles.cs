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
    private float minStartSpeed, maxStartSpeed;

    public float something;
    public Color peakColor;
    public Color noColor;
    public Color RuntimeColor;

    private float velocity;
    private float time;

    [Header("Trail")]
    [SerializeField]
    private TrailRenderer trailRenderer;


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

        Debug.Log(emisson.rateOverDistance);

     //   bigWind.emission.rateOverDistance = 1;
    }

    private void inAir()
    {
        snowstreamR.Pause();
        snowstreamL.Pause();
        snowboardSnowR.Pause();
        snowboardSnowL.Pause();
        smallWind.Pause();

        trailRenderer.time = 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(landingParticles, spawnpoint.transform.position, landingParticles.transform.rotation);
    }
}
