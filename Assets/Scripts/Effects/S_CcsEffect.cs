using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CcsEffect : MonoBehaviour
{
    public GameObject character;
    private GameObject unaffectedCharacter;

    private float durationLeft;
    private void Update()
    {
        durationLeft -= 1 * Time.deltaTime;
        if (durationLeft > 0)
        {
            changeControls();
        }
        if (durationLeft <= 0)
        {
            destroyTheEffect();
        }
    }
    //change controls
    public void changeControls()
    {
        Debug.Log("inverted controlls activated");
    }
    //reset controls after a couple seconds
    public void destroyTheEffect()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        character = unaffectedCharacter;
    }
}
