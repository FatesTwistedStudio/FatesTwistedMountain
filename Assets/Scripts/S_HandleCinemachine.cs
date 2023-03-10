using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class S_HandleCinemachine : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private CinemachineCameraOffset vcamOffset;
    private S_HoverboardPhysic player;
    private Rigidbody rb;
    [Range(0, 1)]
    public float OffsetRange;
    [Range(0, 1)]
    public float fovRange;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcamOffset = vcam.GetComponent<CinemachineCameraOffset>();

    }

    // Update is called once per frame
    void Update()
    {
        vcamOffset.m_Offset.z = Mathf.Lerp(0, 0 + rb.velocity.magnitude * OffsetRange, 0.7f);
        vcam.m_Lens.FieldOfView = Mathf.Lerp(68, 68 + rb.velocity.magnitude * fovRange, 0.7f);
    }
}
