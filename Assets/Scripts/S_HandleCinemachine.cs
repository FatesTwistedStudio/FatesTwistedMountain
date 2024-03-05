using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Windows;

public class S_HandleCinemachine : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private CinemachineCameraOffset vcamOffset;
    private S_HoverboardPhysic player;
    private Rigidbody rb;
    [Range(0.01f, 1)]
    public float OffsetRange;
    [Range(0, 1)]
    public float fovRange;
    private Vector2 _Movement;
    private Vector2 _Rotation;
    S_PlayerInput _PlayerInputScript;
    private float currentDutchAngle;

    float rotationSpeed = 10;



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
        OffsetRange = Mathf.Clamp(OffsetRange, 0.01f,1);
        _Movement = _PlayerInputScript._mvn;
        _Rotation = _PlayerInputScript._rotmvn;
        currentDutchAngle = Mathf.Clamp(currentDutchAngle, -20, 20);

        if (_Rotation.x < 0 || _Rotation.x > 0)
        {
            currentDutchAngle += _Rotation.x * rotationSpeed * Time.deltaTime;

        }
        else if (_Rotation.x == 0)
        {
            currentDutchAngle = Mathf.Lerp(currentDutchAngle, 0f , Time.deltaTime * 2);
        }

        vcamOffset.m_Offset.z = Mathf.Lerp(0, 0 + rb.velocity.magnitude * OffsetRange, 0.7f);
        vcam.m_Lens.FieldOfView = Mathf.Lerp(68, 68 + rb.velocity.magnitude * fovRange, 0.7f);

        //vcamOffset.transform.Rotate(new Vector3(0f, 0f, _Movement.x * rotationSpeed * Time.deltaTime));
        vcam.m_Lens.Dutch = currentDutchAngle;


    }
    private void FixedUpdate()
    {

    }
}
