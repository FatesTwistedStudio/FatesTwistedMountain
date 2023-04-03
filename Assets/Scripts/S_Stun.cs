using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Stun : MonoBehaviour
{

    Rigidbody rb;

    [Header("Stun")]
    public float stunDuration = 5.0f;
    float stunRemaining = 0.0f;


    float minimumFall;
    bool grounded;
    public bool freeze = false;


    S_Recovery S_Recovery;
    S_HoverboardPhysic S_HoverboardPhysic;
    S_PlayerInput S_PlayerInput;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<S_Recovery>() != null)
        {
            S_Recovery = GetComponent<S_Recovery>();
        }
        else
        {
            Debug.Log("S_Recovery not connected");
        }

        if (GetComponent<S_HoverboardPhysic>() != null)
        {
            S_HoverboardPhysic = GetComponent<S_HoverboardPhysic>();
        }
        else
        {
            Debug.Log("S_HoverboardPhysic not connected");
        }

        if (GetComponent<S_PlayerInput>() != null)
        {
            S_PlayerInput = GetComponent<S_PlayerInput>();
        }
        else
        {
            Debug.Log("S_PlayerInput not connected");
        }


        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        CheckGround();

       if(grounded && rb.velocity.y == 0)
        {
            grounded = false;

            if(minimumFall <= -2)
            {
                Stun();
            }
        }
        else if (grounded)
        {
            if (rb.velocity.y < minimumFall)
                minimumFall = rb.velocity.y;
        }
       if (!grounded)
        {
           minimumFall = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //angle = Vector3.Angle(transform.position, target.position);
        //Debug.Log(angle);
        freeze = false;

        if (stunRemaining > 0.0f)
        {
            stunRemaining = Mathf.Max(stunRemaining - Time.deltaTime, 0.0f);
        }

        if (stunRemaining <= 0.0f)
        {
            S_Recovery = GetComponent<S_Recovery>();
        }
    }

    void CheckGround()
    {
        grounded = Physics.Raycast(transform.position + Vector3.up, -Vector3.up, 1.01f);
    }




    //stun 
    public void Stun()
    {
           stunRemaining = stunDuration;
           freeze = true;
           Debug.Log("Player fell" + stunRemaining);

    }
}
