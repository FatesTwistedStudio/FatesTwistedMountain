using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Recovery : MonoBehaviour
{
    public Quaternion resetXRotation;
    public Camera mainCam;
    public bool hasStarted;
    public bool needsRecovery;
    // Start is called before the first frame update
    void Start()
    {
        
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == true)
        {
            if (gameObject.tag == "Player")
            {
                mainCam.GetComponentInChildren<CinemachineVirtualCamera>().Follow = GetComponent<S_CharInfoHolder>().camFollowPoint.transform;
                mainCam.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = GetComponent<S_CharInfoHolder>().camFollowPoint.transform;

            }
            if (transform.rotation.x >= 90)
            {
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
        transform.SetPositionAndRotation(transform.position, resetXRotation );

    }
}
