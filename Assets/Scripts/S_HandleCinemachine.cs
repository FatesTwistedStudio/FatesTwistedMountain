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
      private Vector2 _Movement;
      S_PlayerInput _PlayerInputScript;

     float rotationSpeed = 1;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcamOffset = vcam.GetComponent<CinemachineCameraOffset>();
    _PlayerInputScript = GetComponent<S_PlayerInput>();


    }

    // Update is called once per frame
    void Update()
    {

            _Movement = _PlayerInputScript._mvn;


        vcamOffset.m_Offset.z = Mathf.Lerp(0, 0 + rb.velocity.magnitude * OffsetRange, 0.7f);
        vcam.m_Lens.FieldOfView = Mathf.Lerp(68, 68 + rb.velocity.magnitude * fovRange, 0.7f);
      //  vcam.m_Lens.m_
      vcamOffset.transform.Rotate(new Vector3(0f, 0f, _Movement.x * rotationSpeed * Time.deltaTime));
      
    }
}
