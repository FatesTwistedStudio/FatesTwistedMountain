using UnityEngine;

public class S_HoverboardPhysic : MonoBehaviour
{
    Rigidbody rb;
    public float horizontalTippingAlert;
    public float verticalTippingAlert;

    [Header ("Movement")]
    public float Height;
    public float jumpForce;
    public float moveForce, turnTorque;

    [Header ("Anchors")]
    public Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    private bool inAir;
    private Vector3 airVelocity;
    private float gravity = -9.81f;
    [SerializeField]
    private float gravityMultiplyer;
    public float minRotX;
    public float minRotY;
    public float minRotZ;
    public float maxRotX;
    public float maxRotY;
    public float maxRotZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(inAir);
        LimitRotation();
        ApplyGravity();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
        }
        rb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.right, ForceMode.VelocityChange);
        rb.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up, ForceMode.VelocityChange);
        
        if (inAir == false && Input.GetButton("Jump"))
        {
            rb.AddForce( transform.up * jumpForce, ForceMode.VelocityChange);      
        }
        if (inAir == true)
        {
            rb.AddForce(-airVelocity);
        }    

    }
   public void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit, Height))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            rb.AddForceAtPosition(transform.up * force * Height, anchor.position, ForceMode.Acceleration);
            inAir = false;
        }
        else
        {
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

    private void ApplyGravity()
    {
        if (inAir)
        {
            airVelocity.y += gravity * gravityMultiplyer * Time.deltaTime;
        }
        else
        {
            airVelocity.y = -1f;
        }

        
    }
}
