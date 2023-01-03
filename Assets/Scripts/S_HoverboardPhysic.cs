using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class S_HoverboardPhysic : MonoBehaviour
{
    [SerializeField]
    Transform orientation;
    Rigidbody rb;
    public float horizontalTippingAlert;
    public float verticalTippingAlert;
    public float Height;

    [Header("Movement")]
    public float maxSpeed;
    [SerializeField]
    public float moveForce;
    public float turnTorque;

    [Header("Directions")]
    [SerializeField]
    private float horizontalMovement;
    [SerializeField]
    private float verticalMovement;
    [SerializeField]
    private Vector2 Movement;
    public bool disableMovement;

    Vector3 moveDirection;
    Vector3 airMoveDirection;
    private float airMovement;

    [Header("Friction")]
    public float frictionCof = 10f;
    public float coefficientOfFriction = 10f;
    public static float mass;
    public float normalForce = mass * 9.8f;
    public float frictionForce = 10f;

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

    [Header("Anchors")]
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
        disableMovement = false;
    }

    void Update()
    {
        slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        myInput();
        HandleDrag();

        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }





    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 0, 0), distanceToGround, Ground);
        MovePlayer();
        Overboard();
        ApplyForce();

        if (!isGrounded)
        {
            ApplyGravity();
        }

    }

    public void OnMove(InputValue value)
    {
        Movement = value.Get<Vector2>();
    }

    private void myInput()
    {
        if (disableMovement)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal"); // Left/Right is horizontal
            verticalMovement = Input.GetAxisRaw("Vertical"); //forward and back is veritcal
            airMovement = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontalMovement = 0; // Left/Right is horizontal
            verticalMovement = 0; //forward and back is veritcal
        }
        if (!isGrounded)
        {
            horizontalMovement = Mathf.Abs(horizontalMovement) * -1;
        }

        moveDirection = orientation.forward * -Movement.x + orientation.right * Movement.y;
        airMoveDirection = orientation.forward * -Movement.x + orientation.right * Movement.y;
    }
   public void ApplyForce()
    {
        rb.AddForce(transform.forward * moveForce, ForceMode.Impulse);
        
        // Get the current forward velocity of the snowboard
        Vector3 forwardVelocity = transform.forward * rb.velocity.magnitude;

        // Calculate the friction force as a vector that is perpendicular to the forward velocity
        Vector3 friction = Vector3.Normalize(-forwardVelocity) * frictionForce;
        /*
        if (turningLeft)
        {
            frictionForce = coefficientOfFriction * normalForce;
        }
        else if (turningRight)
        {
            frictionForce = -coefficientOfFriction * normalForce;
        }
        */
        rb.AddForce(friction, ForceMode.Force);
    }
    public void Overboard()
    {
        disableMovement = true;
        if (transform.up.y < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), Time.deltaTime * 1);
            disableMovement = false;
        }
        /*
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
        */
    }

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveForce, ForceMode.VelocityChange);
            rb.AddTorque(horizontalMovement * turnTorque * transform.up, ForceMode.VelocityChange);
            //transform.Rotate(0, horizontalMovement - verticalMovement, 0);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeDirection.normalized * moveForce, ForceMode.VelocityChange);
            rb.AddTorque(horizontalMovement * turnTorque * transform.up, ForceMode.VelocityChange);
           // transform.Rotate(0, -horizontalMovement + verticalMovement, 0);
        }
        else if (!isGrounded) //in the air
        {
            rb.AddForce(moveDirection.normalized * moveForce, ForceMode.VelocityChange);
            rb.AddTorque(airMovement * turnTorque * transform.forward * airRate, ForceMode.VelocityChange );
            //transform.Rotate(0, -horizontalMovement + verticalMovement, 0);
        }

    }

    private void Jump()
    {

        //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        rb.AddForce(moveDirection.normalized * jumpForce, ForceMode.VelocityChange);
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

    private void ApplyGravity()
    {
        rb.AddForce(Vector3.down * -gravity * gravityMultiplyer, ForceMode.Acceleration );
        //Debug.Log("Applying Gravity");
    }

    /* This wasa for debugging ground checksphere
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - new Vector3(0, 0, 0), distanceToGround);
    }
    */

}
