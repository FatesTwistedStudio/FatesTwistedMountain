using Unity.VisualScripting;
using UnityEngine;
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
    private float Height;
    private float maxSlopeAngle;
    private NavMeshAgent navmesh;
    private CharacterController characterController;
    bool isPlayer;
    private S_PlayerInput _PlayerInputScript;

    [Header("Movement")]
    public bool canMove;
    public float acceleration;
    public float maxSpeed;
    public Vector3 velocity;
    private Vector3 forwardVelocity;

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
    private float maxTimeInAir = 10f;
    [SerializeField]
    private float gravityMultiplier;
    [SerializeField]
    private float maxGravityMultiplier = 2.0f; // Maximum gravity multiplier
    private float originalStepOffset;

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
        canMove = false;
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
        originalStepOffset = characterController.stepOffset;
        disableInput = false;

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
        // rb.AddForce(-transform.right * baseVelocity, ForceMode.VelocityChange);
        // rb.AddForce(-transform.up * baseVelocity, ForceMode.VelocityChange);

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
            // Calculate the lean angle based on input
            float leanAngle = _Movement.x * maxLeanAngle;
            currentLeanAngle = Mathf.Lerp(currentLeanAngle, leanAngle, Time.deltaTime * 5);

            // Apply the lean effect to the character mesh
            Vector3 newRotation = playerModel.localEulerAngles;
            newRotation.y = currentLeanAngle;
            playerModel.localEulerAngles = newRotation;
            SurfaceAlignment();

            // Calculate the movement direction based on the character's forward direction
            Vector3 forwardDirection = -transform.right.normalized;

            // Calculate velocity based on the constant speed and forward direction
            forwardVelocity = forwardDirection * 10;

            // Move the character controller using velocity
            characterController.Move(forwardVelocity * Time.fixedDeltaTime);

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
        moveDirection = transform.forward * -_Movement.x + transform.right * -_Movement.y;
        airMoveDirection = orientation.forward * -_AirRot.x + orientation.right * _AirRot.y;
    }

    private void MovePlayer()
    {
        // Calculate acceleration based on input
        Vector3 accelerationVector = moveDirection * acceleration * Time.fixedDeltaTime;

        // Apply acceleration to velocity
        velocity += accelerationVector.normalized;

        // Limit velocity to maximum speed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        characterController.Move(velocity * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        StartCoroutine(JumpCoroutine());

        // Move the character controller to apply the jump force
        characterController.Move(moveDirection);
    }

    IEnumerator JumpCoroutine()
    {
        Debug.Log("Starting Jump");
        // Calculate the initial jump force
        float initialJumpForce = Mathf.Sqrt(2 * Mathf.Abs(_baseGravity) * jumpForce);
        float remainingJumpForce = jumpForce;
        Vector3 jumpMoveDirection = new Vector3(0, 0, 0);

        // Apply jump force gradually over a set number of frames
        for (int i = 0; i < jumpFrames; i++)
        {
            remainingJumpForce += remainingJumpForce / jumpFrames;
            jumpMoveDirection.y += remainingJumpForce / jumpFrames;
            characterController.Move(jumpMoveDirection);
            yield return new  WaitForFixedUpdate();
            Debug.Log("Done");
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
        if (!characterController.isGrounded)
        {
            characterController.stepOffset = 0;
        }
        else
        {
            characterController.stepOffset = originalStepOffset;
        }

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
