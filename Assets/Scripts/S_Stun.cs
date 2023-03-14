using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Stun : MonoBehaviour
{

    public float stunDuration = 5.0f;
    float stunRemaining = 0.0f;


    S_Recovery S_Recovery;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (stunRemaining > 0.0f)
        {
            stunRemaining = Mathf.Max(stunRemaining - Time.deltaTime, 0.0f);
        }

        if (stunRemaining <= 0.0f)
        {
            S_Recovery = GetComponent<S_Recovery>();
        }
    }


    //stun 
    public void Stun()
    {
        stunRemaining = stunDuration;
    }

}
