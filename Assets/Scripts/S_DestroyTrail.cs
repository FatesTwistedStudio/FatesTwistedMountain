using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DestroyTrail : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent == null)
        {
            Invoke("Destroy", 1);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
