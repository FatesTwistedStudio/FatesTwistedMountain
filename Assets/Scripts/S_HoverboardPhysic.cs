using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class S_HoverboardPhysic : MonoBehaviour
{
    [SerializeField]
    Transform orientation;
    Rigidbody rb;
    public float horizontalTippingAlert;
    public float verticalTippingAlert;
    public float Height;

    [Header ("Movement")]
    public float maxSpeed;
    [SerializeField]
    public float moveForce;
    public float turnTorque;
    private float horizontalMovement;
    private float verticalMovement;
    Vector3 moveDirection;
    


    public ParticleSystem sp;
    public AudioSource snowboardSFX;
    private bool playedAudio;
    private bool m_Play;

    [SerializeField]
    private LayerMask Ground;
    [SerializeField]
    private float distanceToGround;

    public float groundRate = 5.0f;
    public float airRate = 0;

    [Header ("Anchors")]
    public Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    [Header("Drag")]
    [SerializeField]
    private float groundDrag = 3f;
    [SerializeField]
    private float groundAngDrag = 3f;
    [SerializeField]
    private float airDrag = 1f;
    [SerializeField]
    private float airAngDrag = 1f;


    private bool inAir;
    private bool isGrounded;
    public float jumpForce;

    private float gravity = -20;
    private float groundedGravity = -5f;
    
    [SerializeField]
    private float maxFallSpeed;
    [SerializeField]
    private float gravityMultiplyer;

    Vector3 slopeDirection;
    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, Height / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    float _verticalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        LimitRotation();
        myInput();
        HandleDrag();

        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }
        
        if (!isGrounded)
        {
            ApplyGravity();
        }
        slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 0, 0), distanceToGround, Ground);
        MovePlayer();

        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
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

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveForce * maxSpeed, ForceMode.VelocityChange);
            rb.AddTorque(horizontalMovement * turnTorque * transform.up, ForceMode.VelocityChange);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeDirection.normalized * moveForce * maxSpeed, ForceMode.VelocityChange);
            rb.AddTorque(horizontalMovement * turnTorque * transform.up, ForceMode.VelocityChange);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveForce * maxSpeed * airRate, ForceMode.VelocityChange);
            rb.AddTorque(horizontalMovement * turnTorque * transform.up, ForceMode.VelocityChange);
        }

    }

    private void Jump()
    {

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
    }

    private void LimitRotation()
    {
        Vector3 playerEulerAngles = gameObject.transform.rotation.eulerAngles;



        gameObject.transform.rotation = Quaternion.Euler(playerEulerAngles);
    }
    private void HandleDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
            rb.angularDrag = groundAngDrag;
        }
        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngDrag;
        }
    }

    private void myInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * -horizontalMovement + orientation.right * verticalMovement;
    }

    private void ApplyGravity()
    {
        rb.AddForce(Physics.gravity, ForceMode.Acceleration );
        Debug.Log("Applying Gravity");
    }

    /* This wasa for debugging ground checksphere
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - new Vector3(0, 0, 0), distanceToGround);
    }
    */

}
