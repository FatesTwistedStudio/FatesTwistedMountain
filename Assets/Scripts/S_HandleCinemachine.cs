using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class S_HandleCinemachine : MonoBehaviour
{
    private CinemachineVirtualCamera m_Camera;
    private S_HoverboardPhysic player;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_Camera = FindObjectOfType<CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update()
    {
        m_Camera.m_Lens.FieldOfView = Mathf.Lerp(68, 68 + rb.velocity.magnitude, 0.2f);
    }
}
