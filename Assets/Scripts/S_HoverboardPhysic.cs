using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
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
    public float rotationSpeed;
    [SerializeField]
    public float moveForce;
    public float bounceForce;
    public float turnTorque;

    private float horizontalMovement;
    private float verticalMovement;
    private Vector2 _Movement;
    private Vector2 _Rotation;
    public bool disableInput;

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        disableInput = false;
    }

    void Update()
    {
        slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        myInput();
        HandleDrag();
        HandleRotation();
    }

    void FixedUpdate()
    {
        // rb.AddForce(moveDirection.normalized * moveForce, ForceMode.VelocityChange);
        CheckGround();

        MovePlayer();
        
        Overboard();
        ApplyForce();

        if (!isGrounded)
        {
            ApplyGravity();
            disableInput = true;
        }
        else
        {
            disableInput = false;


        }

    }

    public void OnMove(InputValue value)
    {
            _Movement = value.Get<Vector2>();
        if (!disableInput)
        {
        }
        else
        {
          //  _Movement = new Vector2(0, 0);
        }
        
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 0, 0), distanceToGround, Ground);
    }

    public void OnJump(InputValue value)
    {
        if(isGrounded)
        {
            Jump();
        }
    }

    public void OnRotate(InputValue value)
    {
        _Rotation = value.Get<Vector2>();
        
    }
    private void HandleRotation()
    {
        GetComponent<Transform>().Rotate(Vector3.up * _Rotation.x * rotationSpeed * 0.2f);
        //LOL
    }

    private void myInput()
    {
        
        if (!isGrounded)
        {
            horizontalMovement = Mathf.Abs(horizontalMovement) * -1;
        }

        moveDirection = orientation.forward * -_Movement.x + orientation.right * _Movement.y;
        airMoveDirection = orientation.forward * -_Movement.x + orientation.right * _Movement.y;
    }
   public void ApplyForce()
    {
        rb.AddForce(transform.forward * moveForce, ForceMode.Impulse);
        
        // Get the current forward velocity of the snowboard
        Vector3 forwardVelocity = transform.forward * rb.velocity.magnitude;

        // Calculate the friction force as a vector that is perpendicular to the forward velocity
        Vector3 friction = Vector3.Normalize(-forwardVelocity) * frictionForce;

        rb.AddForce(friction, ForceMode.Force);
    }
    public void Overboard()
    {
        disableInput = true;
        if (transform.up.y < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), Time.deltaTime * 1);
            disableInput = false;
        }
    }

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveForce, ForceMode.VelocityChange);
         //   rb.AddTorque(Movement.x * turnTorque * transform.up, ForceMode.VelocityChange);
            //transform.Rotate(0, horizontalMovement - verticalMovement, 0);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeDirection.normalized * moveForce, ForceMode.VelocityChange);
          //  rb.AddTorque(Movement.x * turnTorque * transform.up, ForceMode.VelocityChange);
           // transform.Rotate(0, -horizontalMovement + verticalMovement, 0);
        }
        else if (!isGrounded) //in the air
        {
            rb.AddForce(moveDirection.normalized * moveForce, ForceMode.VelocityChange);
           // rb.AddTorque(airMovement * turnTorque * transform.forward * airRate, ForceMode.VelocityChange );
            //transform.Rotate(0, -horizontalMovement + verticalMovement, 0);
        }

    }

    private void Jump()
    {

        //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        rb.AddForce(_Movement.normalized * jumpForce, ForceMode.VelocityChange);
        disableInput = true;
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
        //rb.velocity += Vector3.down * newGravity * Time.deltaTime;
        //Debug.Log("Applying Gravity");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision normal vector is facing upwards
        if (collision.contacts[0].normal.y > 0)
        {
            // Calculate the bounce force direction
            Vector3 bounceDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            // Apply the bounce force
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }

    }


    /* This wasa for debugging ground checksphere
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - new Vector3(0, 0, 0), distanceToGround);
    }
    */

}
