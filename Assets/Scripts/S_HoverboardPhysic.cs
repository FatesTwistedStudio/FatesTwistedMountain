using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;
using System.Collections;

public class S_HoverboardPhysic : MonoBehaviour
{
    [Header("Base Components : Please Connect")]
    [SerializeField]
    private Transform orientation;
    [SerializeField]
    private Transform playerModel;
    [SerializeField]
    private float Height;
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    private float slopeMovementMultiplier;
    [SerializeField]
    private float raycastOffsetX;
    private NavMeshAgent navmesh;
    private CharacterController characterController;
    bool isPlayer;
    private S_PlayerInput _PlayerInputScript;
    private S_RailGrinding railGrinding;

    [Header("Movement")]
    public bool canMove;
    public float acceleration;
    public float accelerationRate;
    public float maxSpeed;
    public Vector3 velocity;
    private Vector3 forwardVelocity;
    [SerializeField]
    private Vector3 lastInputDirection = Vector3.zero;

    [Header("Drifting")]
    public float driftForce = 10f;
    public float maxDriftDuration = 2f;
    private float currentDriftDuration = 0f;
    private bool isDrifting = false;

    [Header("Jumping")]
    [SerializeField]
    private float jumpForce;
    private float currentLeanAngle = 0;
    public float maxLeanAngle = 45.0f;
    public float jumpFrames = 10f;
    private float jumpingDelay = 1f;
    private bool cantJump;

    [Header("Rotation")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float airRotationSpeed;

    [Header("Input Vectors")]
    private Vector2 _Movement;
    private Vector2 _Rotation;
    private Vector2 _AirRot;
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
    private float maxTimeInAir = 10f;
    [SerializeField]
    private float gravityMultiplier;
    [SerializeField]
    private float maxGravityMultiplier = 2.0f; // Maximum gravity multiplier
    private float originalStepOffset;

    RaycastHit slopeHit;

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position + transform.right * raycastOffsetX , Vector3.down, out slopeHit, Height * 1f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle > maxSlopeAngle;
        }
        return false;
    }

    void Update()
    {
         Debug.DrawRay(transform.position + transform.right * raycastOffsetX , Vector3.down * (Height * 1f), Color.green); // Visualize the raycast
        if (OnSlope())
        {
            //Debug.Log("On Slope");
        }
        else
        {
            //Debug.Log("L + Ratio");
        }
    }

    void Start()
    {
        //canMove = false;
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
        characterController = GetComponent<CharacterController>();
        railGrinding = GetComponent<S_RailGrinding>();
        originalStepOffset = characterController.stepOffset;

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if(isPlayer)
            {
                _Movement = _PlayerInputScript._mvn;
                _Rotation = _PlayerInputScript._rotmvn;
                _AirRot = _PlayerInputScript._rotair;
            }

            // Calculate the rotation
            //Debug.Log(rotation + _Rotation.x + _Rotation.y);
            float rotation = _Rotation.x * rotationSpeed * Time.fixedDeltaTime;
            transform.Rotate(0, rotation, 0);

            myInput();
            MovePlayer();
            ApplyGravity();

            if (characterController.isGrounded)
            {

                //disableInput = false;
                // Reset time in air and gravity multiplier
                currentTimeInAir = 0f;
                gravityMultiplier = 1.0f;

            }
            else
            {
                //HandleAir();
                // Increment time in air
                currentTimeInAir += Time.fixedDeltaTime;

                // Increase gravity multiplier over time
                gravityMultiplier = Mathf.Lerp(1.0f, maxGravityMultiplier, currentTimeInAir / maxTimeInAir);
            }

            SurfaceAlignment();
            //

            if (isDrifting)
            {
                // Apply drift force while drifting
                ApplyDriftForce();

                // Update drift duration
                currentDriftDuration += Time.fixedDeltaTime;
                if (currentDriftDuration >= maxDriftDuration)
                {
                    EndDrift();
                }
                Debug.Log(currentDriftDuration);
            }
        }

    }

    public void Overboard()
    {
        //When the player is overboard the player model resets it rotation to being back upright.
        //disableInput = true;
        if (transform.up.y < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), Time.deltaTime * 1);
            //disableInput = false;
        }
    }

    private void myInput()
    {
        //Applying the input values given by the Player Input script to a usable Vector 3. Y Movement is clamped to prevent the player from moving backwards.
        _Movement.y = Mathf.Clamp(_Movement.y, 0, 1);
        if(_Movement.magnitude > 0)
        {
            lastInputDirection = new Vector3(_Movement.x, 0, _Movement.y).normalized;
        }
        //Debug.Log(_Movement.y);
        moveDirection = transform.forward * -_Movement.x + transform.right * -_Movement.y;
        airMoveDirection = orientation.forward * -_AirRot.x + orientation.right * _AirRot.y;
    }

    private void MovePlayer()
    {
        if (railGrinding.onRail == false)
        {
            if(_Movement.y < 1)
            {
                acceleration -= Time.fixedDeltaTime * accelerationRate;
                ForwardMovement();

            }
            else
            {
                acceleration += Time.fixedDeltaTime * accelerationRate;
            }
            if (OnSlope())
            {
                Debug.Log("On Slope");
                // Apply slope movement multiplier to restrict uphill movement
                // Calculate the angle between the player's forward direction and the slope normal
                float angleToSlope = Vector3.Angle(transform.forward, slopeHit.normal);

                // Determine if the slope is facing the opposite direction of the player's forward movement
                bool isSlopeBackward = angleToSlope > 90f;

                // Adjust movement direction based on the slope
                if (isSlopeBackward)
                {
                    // Move backward on slopes facing away from the player's forward direction
                    acceleration *= slopeMovementMultiplier;
                }
                else
                {
                    // Move forward on slopes facing toward the player's forward direction
                    acceleration *= 1;
                }
            }
            acceleration = Mathf.Clamp(acceleration, 0.4f, maxSpeed);

            // Calculate acceleration based on input
            Vector3 accelerationVector = moveDirection * acceleration;

            // Apply acceleration to velocity
            velocity = accelerationVector;

            // Limit velocity to maximum speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

            characterController.Move(velocity * Time.fixedDeltaTime);
        }
        else
        {
            //characterController.Move(railGrinding.grindSpeed);

        }

    }

    private void ForwardMovement()
    {
        // Calculate the movement direction based on the character's forward direction
        Vector3 forwardDirection = -transform.right;

        if (_Movement.y < 1)
        {
            forwardVelocity = forwardDirection * acceleration;
        }
        else
        {
            forwardVelocity = forwardDirection;
            //Debug.Log(forwardVelocity);
        }

        // Move the character controller using velocity
        characterController.Move(forwardVelocity * Time.fixedDeltaTime);
    }


    public void Jump()
    {
        if (canMove)
        {
            StartCoroutine(JumpCoroutine());
            // Move the character controller to apply the jump force
            characterController.Move(moveDirection);
        }

    }

    IEnumerator JumpCoroutine()
    {
        // Calculate the initial jump force
        float initialJumpForce = Mathf.Sqrt(Mathf.Abs(_baseGravity) * jumpForce);
        float remainingJumpForce = initialJumpForce;
        Vector3 jumpMoveDirection = new Vector3(0, 0, 0);

        // Apply jump force gradually over a set number of frames
        for (int i = 0; i < jumpFrames; i++)
        {
            remainingJumpForce += remainingJumpForce / jumpFrames;
            jumpMoveDirection.y = remainingJumpForce / jumpFrames;
            characterController.Move(jumpMoveDirection);
            yield return new  WaitForFixedUpdate();
        }
    }

    private void ApplyDriftForce()
    {
        // Calculate drift force direction based on the character's forward direction
        Vector3 driftForceDirection = -transform.right.normalized;

        // Apply drift force by moving the character
        characterController.Move(driftForceDirection * driftForce * Time.fixedDeltaTime);
    }

    public void StartDrift()
    {
        isDrifting = true;
        currentDriftDuration = 0f;
    }

    private void EndDrift()
    {
        isDrifting = false;
        // Add any actions to perform when the drift ends
    }

    private void resetJumpDelay()
    {
        cantJump = false;
    }

    private void HandleAir()
    {
        _Movement.y = Mathf.Clamp(_Movement.y, 0, 1);
        moveDirection = orientation.forward * -_Movement.x + orientation.right * -_Movement.y;

        // Include the existing vertical movement (jumping)
        moveDirection.y += Physics.gravity.y * Time.fixedDeltaTime;

        //disableInput = true;
    }

    private void ApplyGravity()
    {
        moveDirection.y += _baseGravity * gravityMultiplier;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void SurfaceAlignment()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, distanceToGround, Ground))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            float angleToGround = Vector3.Angle(transform.up, hit.normal);

            if (angleToGround > maxLeanAngle)
            {
                targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angleToGround - maxLeanAngle);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }
    }

}
