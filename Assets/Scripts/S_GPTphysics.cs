using UnityEngine;
public class S_GPTphysics : MonoBehaviour
{
    // The speed at which the snowboarder moves
    public float speed = 10.0f;
    // The force applied to the snowboarder when they jump
    public float jumpForce = 10.0f;
    // The maximum angle that the snowboarder can tilt at
    public float maxTiltAngle = 30.0f;
    // The smoothness of the tilt effect
    public float tiltSmoothness = 5.0f;

    // A reference to the snowboarder's rigidbody component
    private Rigidbody rb;
    // A flag to track whether the snowboarder is on the ground
    private bool isGrounded = false;
    // A flag to track whether the snowboarder is in the air
    private bool isJumping = false;
    // A timer to keep track of how long the snowboarder has been in the air
    private float jumpTimer = 0.0f;
    // The current angle of tilt of the snowboarder
    private float tiltAngle = 0.0f;

    void Start()
    {
        // Get a reference to the rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the snowboarder is on the ground
        if (isGrounded)
        {
            // If the space key is pressed, set the isJumping flag and apply a force to the snowboarder
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        // If the snowboarder is in the air, update the jump timer
        else if (isJumping)
        {
            jumpTimer += Time.deltaTime;
        }
        // If the snowboarder has been in the air for more than 0.5 seconds, set the isJumping flag to false
        if (jumpTimer > 0.5f)
        {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        // Get the current velocity of the snowboarder
        Vector3 velocity = rb.velocity;
        // Get the current position of the snowboarder
        Vector3 position = transform.position;

        // Check if the left or right arrow keys are pressed
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // If the left arrow key is pressed, tilt the snowboarder to the left and move it to the left
            tiltAngle = Mathf.Lerp(tiltAngle, -maxTiltAngle, tiltSmoothness * Time.deltaTime);
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // If the right arrow key is pressed, tilt the snowboarder to the right and move it to the right
            tiltAngle = Mathf.Lerp(tiltAngle, maxTiltAngle, tiltSmoothness);
        }
    }
}
