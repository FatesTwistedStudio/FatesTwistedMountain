using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class S_HoverboardPhysic : MonoBehaviour
{
    [SerializeField]
    private Transform orientation;
    [SerializeField]
    private Transform playerModel;
    Rigidbody rb;
    private float Height;
    private float maxSlopeAngle;

    bool isPlayer;
    private S_PlayerInput _PlayerInputScript;

    [Header("Movement")]
    [SerializeField]
    private float baseVelocity = 10.0f;
    private PlayerInput playerInput;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float moveForce;
    [SerializeField]
    private float slowForce;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float airRotationSpeed;
    [SerializeField]
    private float slopeForce;
    public float bounceForce;
    public float turnTorque;
    [SerializeField]
    private AnimationCurve animCurve;
    [SerializeField]
    private float snapTime;

    private Vector2 _Movement;
    private Vector2 _Rotation;
    private Vector2 _AirRot;
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
    public bool isGrounded;
    public float jumpForce;

    [SerializeField]
    private float _baseGravity = -20;
    private float currentTimeInAir = 0f;

    [SerializeField]
    private float maxFallSpeed;
    [SerializeField]
    private float gravityMultiplyer;

    Vector3 slopeDirection;
    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, Height * 1f ))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    void Start()
    {
        if (gameObject.GetComponent<S_PlayerInput>() != null)
        {
          //  Debug.Log("Player Alert");
            isPlayer = true;
            _PlayerInputScript = GetComponent<S_PlayerInput>();

        }
        else
        {
           // Debug.Log("im a cpu robot mannnnn");
            isPlayer = false;
        }
        rb = GetComponent<Rigidbody>();
        
        disableInput = false;
    }

    void Update()
    {
        HandleDrag();
        myInput();
    }

    void FixedUpdate()
    {
        if(isPlayer)
        {
            _Movement = _PlayerInputScript._mvn;
            _Rotation = _PlayerInputScript._rotmvn;
            _AirRot = _PlayerInputScript._rotair;
        }

        rb.AddForce(-transform.right * baseVelocity, ForceMode.VelocityChange);
        rb.AddForce(-transform.up * baseVelocity, ForceMode.VelocityChange);

        currentTimeInAir = Mathf.Clamp(currentTimeInAir, 0f, 2f);

        CheckGround();
        MovePlayer();
        Overboard();
        ApplyForce();

        if (!isGrounded)
        {
            HandleAir();
            rb.velocity += moveDirection * Time.fixedDeltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), Time.deltaTime * 1);

        }
        else if (isGrounded)
        {
            HandleRotation();
          
            playerModel.rotation = Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), Time.deltaTime * 1.5f);

            disableInput = false;
            currentTimeInAir -= Time.deltaTime * 3.5f;
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 0, 0), distanceToGround, Ground);
    }

    private void HandleRotation()
    {
        GetComponent<Transform>().Rotate(Vector3.up * _Rotation.x * rotationSpeed * 0.2f);
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
        float moveSpeed = _Movement.y > 0 ? moveForce : slowForce;

        if (isGrounded && !OnSlope())
        {
            rb.AddForce((-moveDirection.normalized * moveForce * moveSpeed), ForceMode.VelocityChange);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(-moveDirection.normalized * moveForce * moveSpeed, ForceMode.VelocityChange);
        }
        else if (!isGrounded) //in the air
        {
            rb.AddForce(-moveDirection.normalized * moveForce, ForceMode.VelocityChange);
        }

    }

    public void Jump()
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
    private void myInput()
    {
        moveDirection = orientation.forward * -_Movement.x + orientation.right * _Movement.y;
        // slopeDirection = (orientation.forward * -_Movement.x * -GetSlopeMoveDirection().x)+ (orientation.right * _Movement.y * -GetSlopeMoveDirection().z);
        airMoveDirection = orientation.forward * -_AirRot.x + orientation.right * _AirRot.y;
    }

    private void ApplyGravity()
    {
        float gravity = _baseGravity * Mathf.Pow(gravityMultiplyer * currentTimeInAir, currentTimeInAir);
        rb.velocity += Vector3.down * -gravity *(gravityMultiplyer * Time.deltaTime*3f);
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
    private void HandleAir()
    {

        playerModel.GetComponent<Transform>().Rotate(Vector3.up * _AirRot.x * airRotationSpeed * 1f);
        playerModel.GetComponent<Transform>().Rotate(Vector3.forward * _AirRot.y * airRotationSpeed * 1f);

        currentTimeInAir += Time.deltaTime;

        ApplyGravity();
        disableInput = true;
    }


    /* This was for debugging ground checksphere
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - new Vector3(0, 0, 0), distanceToGround);
    }
    */

}
