using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StartOptions : MonoBehaviour
{
    public Animator anim;

    public void Start() 
    {

    }

    public void EnableOptions()
    {
        if (anim.GetBool("IsOptionEnabled") == true)
        {
            anim.SetBool("IsOptionEnabled", false);
            anim.SetBool("IsCreditsEnabled", false);
            anim.Play("a_SM_DisableOptions");
        }
        else
        {
            anim.SetBool("IsCreditsEnabled", false);
            anim.SetBool("IsOptionEnabled", true);
            anim.Play("a_SM_ShowOptions");
        }

    }
}
