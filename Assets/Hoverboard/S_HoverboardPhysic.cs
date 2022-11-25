using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HoverboardPhysic : MonoBehaviour
{
    Rigidbody hb;
    public float horizontalTippingAlert;
    public float verticalTippingAlert;

    public float multiplier;
    public float moveForce, turnTorque;

    public Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    void Start()
    {
        hb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
        }
        hb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.forward);
        hb.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up);
    }
   public void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            hb.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);
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
}
