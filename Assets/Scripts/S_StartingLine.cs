using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StartingLine : MonoBehaviour
{
    private MeshRenderer renderer;
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }
    private void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {


    }
    // Update is called once per frame
    void Update()
    {

    }
}
