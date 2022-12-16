using UnityEngine;

public class S_HoverboardPhysic : MonoBehaviour
{
    Rigidbody rb;
    public float horizontalTippingAlert;
    public float verticalTippingAlert;
    public float Height;

    [Header ("Movement")]
    public float maxSpeed;
    [SerializeField]
    public float moveForce;
    public float turnTorque;

    public ParticleSystem sp;
    public AudioSource snowboardSFX;
    private bool playedAudio;
    private bool m_Play;

    public float groundRate = 5.0f;
    public float airRate = 0;

    [Header ("Anchors")]
    public Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    private bool inAir;
    public float jumpForce;

    private float gravity = -9.81f;
    [SerializeField]
    private float gravityMultiplyer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        LimitRotation();

        if (inAir == true)
        {
            rb.AddForce(transform.up * (gravity * gravityMultiplyer));
            //Debug.Log("Applying Gravity");

        }

    }

    void FixedUpdate()
    {

        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
        }
        rb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.right, ForceMode.VelocityChange );
        rb.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up, ForceMode.VelocityChange);


        if (inAir == false && Input.GetButton("Jump"))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
            //Debug.LogWarning("Player has pressed Jump");
            inAir = true;
        }

    }
   public void ApplyForce(Transform anchor, RaycastHit hit)
    {
        var emission = sp.emission;

        if (Physics.Raycast(anchor.position, -anchor.up, out hit, Height))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            rb.AddForceAtPosition(transform.up * force * Height, anchor.position, ForceMode.Acceleration);
            inAir = false;


            emission.rateOverDistance = groundRate;
            
        }
        else
        {
            emission.rateOverDistance = airRate;
            inAir = true;
        }
    }
    public void overboardControls()
    {
        if (GetComponent<Transform>().rotation.x > horizontalTippingAlert)
        {
           
        }
        if (GetComponent<Transform>().rotation.x > -horizontalTippingAlert)
        {

        }
        if (GetComponent<Transform>().rotation.z > verticalTippingAlert)
        {

        }
        if (GetComponent<Transform>().rotation.z > -verticalTippingAlert)
        {

        }
    }

    private void LimitRotation()
    {
        Vector3 playerEulerAngles = gameObject.transform.rotation.eulerAngles;



        gameObject.transform.rotation = Quaternion.Euler(playerEulerAngles);
    }
}
