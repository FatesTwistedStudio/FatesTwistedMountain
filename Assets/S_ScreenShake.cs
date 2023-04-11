using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScreenShake : MonoBehaviour
{
    public Animator anim;

    public void Shake()
    {
        anim.SetTrigger("Shake");

    }

}
