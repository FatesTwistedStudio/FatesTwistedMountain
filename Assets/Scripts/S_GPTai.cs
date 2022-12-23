using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GPTai : MonoBehaviour
{
    // The speed at which the AI snowboarder moves
    public float speed = 10.0f;
    // The force applied to the AI snowboarder when they jump
    public float jumpForce = 10.0f;
    // The maximum angle that the AI snowboarder can tilt at
    public float maxTiltAngle = 30.0f;
    // The smoothness of the tilt effect
    public float tiltSmoothness = 5.0f;
    // The minimum distance at which the AI snowboarder will jump
    public float jumpDistance = 5.0f;

    // A reference to the AI snowboarder's rigidbody component
    private Rigidbody rb;
    // A flag to track whether the AI snowboarder is on the ground
    private bool isGrounded = false;
    // A timer to keep track of how long the AI snowboarder has been in the air
    private float jumpTimer = 0.0f;
    // The current angle of tilt of the AI snowboarder
    private float tiltAngle = 0.0f;
    // The target position for the AI snowboarder to move towards
    private Vector3 targetPosition;

    void Start()
    {
        // Get a reference to the rigidbody component
        rb = GetComponent<Rigidbody>();
        // Set the initial target position
       // SetTargetPosition();
    }

    void Update()
    {
        // Calculate the distance between the AI snowboarder and the target position
        float distance = Vector3.Distance(transform.position, targetPosition);
        // If the distance is less than the jump distance and the AI snowboarder is on the ground, jump
        if (distance < jumpDistance && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpTimer = 0.0f;
        }
        // If the AI snowboarder is in the air, update the jump timer
        else if (!isGrounded)
        {
            jumpTimer += Time.deltaTime;
        }
        // If the AI snowboarder has been in the air for more than 0.5 seconds, set the target position to the current position
        if (jumpTimer > 0.5f)
        {
            targetPosition = transform.position;
        }
    }

    void FixedUpdate()
    {
        // Calculate the direction to the target position
        Vector3 direction = targetPosition - transform.position;
        // Normalize the direction
        direction.Normalize();
        // Calculate the desired velocity
        Vector3 desiredVelocity = direction * speed;
        // Calculate the steering force
        Vector3 steeringForce = desiredVelocity - rb.velocity;
        // Apply the steering force to the AI snowboarder
        rb.AddForce(steeringForce);

        // Calculate the angle between the AI snowboarder's forward direction and the direction to the target position
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        // Tilt the AI snowboarder based on the angle
    }

}
