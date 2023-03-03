using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class S_HoverboardPhysic : MonoBehaviour
{
    [Header("Base Components : Please Connect")]
    [SerializeField]
    private Transform orientation;
    [SerializeField]
    private Transform playerModel;
    Rigidbody rb;
    private float Height;
    private float maxSlopeAngle;
    private NavMeshAgent navmesh;

    bool isPlayer;
    private S_PlayerInput _PlayerInputScript;

    [Header("Movement")]
    [SerializeField]
    private float baseVelocity;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float moveForce;
    [SerializeField]
    private float jumpForce;
 
    [Header("Rotation")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float airRotationSpeed;

    [Header("Input Vectors")]
    private Vector2 _Movement;
    private Vector2 _Rotation;
    private Vector2 _AirRot;
    public bool disableInput;
    private PlayerInput playerInput;

    Vector3 moveDirection;
    Vector3 airMoveDirection;

    [Header("Audio")]
    public AudioSource snowboardSFX;
    private bool playedAudio;
    private bool m_Play;

    [Header("Grounding")]
    [SerializeField]
    private LayerMask Ground;
    [SerializeField]
    private float distanceToGround;
    public bool isGrounded;


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

    [Header("Gravity")]
    [SerializeField]
    private float _baseGravity = -20;
    private float currentTimeInAir = 0f;
    [SerializeField]
    private float gravityMultiplyer;

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
            isPlayer = true;
            _PlayerInputScript = GetComponent<S_PlayerInput>();
            navmesh = GetComponent<NavMeshAgent>();
            navmesh.enabled = false;
        }
        else
        {
            isPlayer = false;
        }
      
        rb = GetComponent<Rigidbody>();
        
        disableInput = false;
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
        
        myInput();
        HandleDrag();
        MovePlayer();

        CheckGround();
        Overboard();

        if (!isGrounded)
        {
            HandleAir();
            rb.velocity += moveDirection * Time.fixedDeltaTime;
        }
        if (isGrounded)
        {
            HandleRotation();
        
            disableInput = false;
            currentTimeInAir -= Time.deltaTime * 3.5f;


        }

    }

    private void CheckGround()
    {
        //Checks to see if the player is grounded by drawing a sphere at the Player's foot.
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 0, 0), distanceToGround, Ground);
    }

    private void HandleRotation()
    {
        rb.AddRelativeTorque(Vector3.up * _Rotation.x * rotationSpeed * 0.2f, ForceMode.VelocityChange);

        //transform.Rotate(Vector3.up * _Rotation.x * rotationSpeed * 0.2f);
        playerModel.transform.Rotate(Vector3.right * _Rotation.x * rotationSpeed * 0.1f);
    }

    public void Overboard()
    {
        //When the player is overboard the player model resets it rotation to being back upright.
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
            rb.AddForce((-moveDirection.normalized * moveForce), ForceMode.VelocityChange);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(-moveDirection.normalized * moveForce , ForceMode.VelocityChange);
        }
        else if (!isGrounded) //in the air
        {
            rb.AddForce(-moveDirection.normalized * moveForce, ForceMode.VelocityChange);
        }

    }

    public void Jump()
    {
        //Force applied to player when they press the jump button
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        rb.AddForce(_Movement.normalized * jumpForce, ForceMode.VelocityChange);
    }

    private void HandleDrag()
    {
        //Adjusts the drag of the player's rigid body depending on if they are grounded or not.
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
        //Applying the input values given by the Player Input script to a usable Vector 3. Y Movement is clamped to prevent the player from moving backwards.
        _Movement.y = Mathf.Clamp(_Movement.y, 0, 1);
        moveDirection = orientation.forward * -_Movement.x + orientation.right * _Movement.y;
        airMoveDirection = orientation.forward * -_AirRot.x + orientation.right * _AirRot.y;
    }

    private void HandleAir()
    {
        playerModel.GetComponent<Transform>().Rotate(Vector3.up * _AirRot.x * airRotationSpeed * 1f);
        playerModel.GetComponent<Transform>().Rotate(Vector3.forward * _AirRot.y * airRotationSpeed * 1f);

        currentTimeInAir += Time.deltaTime;

        ApplyGravity();
        disableInput = true;

    }

    private void ApplyGravity()
    {
        //Applying gravity to the player when they leave the ground. Gets stronger over time.
        float gravity = _baseGravity * Mathf.Pow(gravityMultiplyer * currentTimeInAir, currentTimeInAir);
        rb.velocity += Vector3.down * -gravity * (gravityMultiplyer * Time.deltaTime * 3f);
    }
}