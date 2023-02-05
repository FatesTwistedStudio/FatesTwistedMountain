using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HovEffect : MonoBehaviour
{
    public GameObject character;
    private GameObject unaffectedCharacter;

    public float minDuration;
    public float maxDuration;
    private float durationLeft;

    private void Start()
    {
        if (character != null)
        {
            unaffectedCharacter = character;
        }
        durationRandomizer();
    }
    private void Update()
    {
        durationLeft = Time.deltaTime;
        if (durationLeft > 0)
        {
            offTheGround();
            decreaseMass();
        }
        if (durationLeft <= 0)
        {
            destroyTheEffect();
        }
    }
    // avoid ground based effects
    public void offTheGround()
    {
        Debug.Log("Needs To Hover");
    }
    // less mass
    private void decreaseMass()
    {
        Debug.Log("Mass should decrease");

    }
    //lasts a couple seconds
    private void durationRandomizer()
    {
        durationLeft = Random.Range(minDuration, maxDuration);
    }
    public void destroyTheEffect()
    {
        if (durationLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
    //for random amount of time
    private void OnDestroy()
    {
        character = unaffectedCharacter;
    }
}
