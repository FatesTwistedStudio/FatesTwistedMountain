using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DebugTimescale : MonoBehaviour
{
    [Range(0.1f,10)]
    public float modifiedScale;

    void Update()
    {
        Time.timeScale = modifiedScale;
    }
}
