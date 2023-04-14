using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Stun : MonoBehaviour
{

    public Transform orientation;
    Rigidbody rb;

    [Header("Stun")]
    public float stunDuration = 5.0f;
    float stunRemaining = 0.0f;
    [SerializeField]
    private LayerMask ground;

    float minimumFall;
    public bool grounded;
    public bool freeze = false;


    S_Recovery S_Recovery;
    S_HoverboardPhysic S_HoverboardPhysic;
    S_PlayerInput S_PlayerInput;

    public Animator anim;

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

        minimumFall = Mathf.Clamp(minimumFall, 0, 11);
       
       if(grounded)
        {
            minimumFall -= Time.deltaTime *2;

        }
       if (!grounded)
        {
          // minimumFall = 0;
           minimumFall += Time.deltaTime * 3;
        }

        Debug.Log(minimumFall);


        Ray ray = new Ray(orientation.transform.position, -orientation.transform.up);
        RaycastHit info = new RaycastHit();

        if (!Physics.Raycast(ray, out info, 1, ground) && grounded && minimumFall >= 10 ||Physics.Raycast(orientation.transform.position, orientation.transform.up, out info, 2, ground))
        {
            minimumFall = 0;
            Stun();
            Debug.LogWarning("Stunned");

        }
        else
        {
        }

    }

    void CheckGround()
    {
        grounded = Physics.CheckSphere(transform.position - new Vector3(0, 0, 0), 1, ground);
    }

    //stun 
    public void Stun()
    {
        anim.SetBool("Wipeout", true);
           stunRemaining = stunDuration;
           freeze = true;
           Debug.Log("Player fell" + stunRemaining);
           Invoke("TurnoffStun", 3);

    }

    public void TurnoffStun()
    {
            anim.SetBool("Wipeout", false);
    }
}
