using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Recovery : MonoBehaviour
{
    public Quaternion resetXRotation;
    public Vector3 resetLocation;
    
    public float yDrop;

    public Camera mainCam;
    public bool hasStarted;
    public bool needsRecovery;
    // Start is called before the first frame update
    void Start()
    {
        resetXRotation=transform.rotation;
        resetLocation.Set(transform.position.x, transform.position.y+yDrop,transform.position.z);
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == true)
        {
            if (gameObject.tag == "Player")
            {
               
            }
            if (transform.rotation.x >= 90)
            {
                Debug.Log("Lean");
                recoveryMethod();
            }
            if (transform.rotation.x <= -90)
            { recoveryMethod(); }
            if (transform.rotation.y >= 90)
            {
                recoveryMethod();
            }
            if (transform.rotation.y <= -90)
            { recoveryMethod(); }
            if (transform.rotation.z >= 90)
            {
                recoveryMethod();
            }
            if (transform.rotation.z <= -90)
            { recoveryMethod(); }

        }


    }
    public void recoveryMethod()
    {
        transform.SetPositionAndRotation(resetLocation, resetXRotation );

    }
}
