using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerController : MonoBehaviour
{
    public float turnSpeed;
    public float speed;

    [Header("Boost")]
    public float normalSpeed;
    public float boostedSpeed;
    public float speedCoolDown;
    public float backdraftDistance;
    public float backdraftTime;
    private float cooldownTimer = Mathf.Infinity;

    public GameObject BL;
    public GameObject FL;
    public GameObject BR;
    public GameObject FR;
    public GameObject Center;

    private Vector3 BLstart;
    private Vector3 FLstart;
    private Vector3 BRstart;
    private Vector3 FRstart;

    //stun 
    public float stunDuration = 5.0f;
    float stunRemaining = 0.0f;

    public Vector3 tiltDegree;

    S_HoverboardPhysic S_HoverboardPhysic;

    S_Recovery S_Recovery;
    void Start()
    {
        if (GetComponent<S_HoverboardPhysic>() != null)
        {
            S_HoverboardPhysic = GetComponent<S_HoverboardPhysic>();
        }
        else
        {
            Debug.Log("S_HoverboardPhysic not connected");
        }
        if (BL != null)
        {
            BLstart = BL.transform.position;
        }
        if (FL != null)
        {
            FLstart = FL.transform.position;
        }
        if (BR != null)
        {
            BRstart = BR.transform.position;
        }
        if (FR != null)
        {
            FRstart = FR.transform.position;
        }
        normalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        leaningBoardHorizantally();



        if (stunRemaining > 0.0f)
        {
            stunRemaining = Mathf.Max(stunRemaining - Time.deltaTime, 0.0f);
        }
            
        if (stunRemaining <= 0.0f)
        {
            S_Recovery = GetComponent<S_Recovery>();
        }

    }

    public void leaningBoardHorizantally()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BL.transform.position = BL.transform.position - tiltDegree;
            FL.transform.position = FL.transform.position - tiltDegree;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            resetAnchors();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            BR.transform.position = BR.transform.position - tiltDegree;
            FR.transform.position = FR.transform.position - tiltDegree;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            resetAnchors();
        }
    }

    public void resetAnchors()
    {
        BL.transform.position = BLstart;
        FL.transform.position = FLstart;
        BR.transform.position = BRstart;
        FR.transform.position = FRstart;
    }

    //stun 
    public void Stun()
    {
        stunRemaining = stunDuration;
    }


    //boost

    /* Need to add this instead of collider trigger 
    cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= backdraftTime)
        {
            cooldownTimer = 0;
    */

    void OnTriggerEnter(Collider collider)
    {
            if (collider.CompareTag("SpeedBoost"))
        {
            speed = boostedSpeed;
            StartCoroutine("SpeedDuration");
        }
    }

    IEnumerator SpeedDuration()
    {
        yield return new WaitForSeconds(speedCoolDown);
        speed = normalSpeed;
    }
}
