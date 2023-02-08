using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GstEffect : MonoBehaviour
{
    public GameObject character;
    private GameObject unaffectedCharacter;
    public float gravityIncrease;
    private float durationLeft;

    private void Start()
    {
        unaffectedCharacter = character;
    }
    private void Update()
    {
        durationLeft -= 1 * Time.deltaTime;

        if (durationLeft <= 0)
        {
            destroyTheEffect();
        }
    }
    //players who touch prefab cant jump
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            increaseGravity(other.gameObject);
        }
        if (other.tag == "Player")
        {
            increaseGravity(other.gameObject);
        }
    }
    private void increaseGravity(GameObject affectedCharacter)
    {
        Debug.Log("increase on gravity");
    }
    //effect lasts couple seconds
    public void destroyTheEffect()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        character = unaffectedCharacter;
    }
}
