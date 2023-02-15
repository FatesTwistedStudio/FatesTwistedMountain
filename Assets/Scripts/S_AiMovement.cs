using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_AiMovement : MonoBehaviour
{
    
    public NavMeshAgent aiCharacter;
    
    public float Speed;

    //focus on items
    private void Update()
    {
        if(gameObject.tag == "Character")
        {

        }
    }
    public void seeItem()
    {
        Debug.Log(gameObject.name + " seees an item");
    }
    public void useItem(GameObject itemToUse)
    {
        Debug.Log(gameObject.name + " uses an item");
    }
    public void discardItem(GameObject itemToDiscard)
    {
        Debug.Log(gameObject.name + " discards an item");

    }
    //focus on shortest path
    public void leanLeft()
    {
        Debug.Log(gameObject.name + " leans left");

    }
    public void leanRight()
    {
        Debug.Log(gameObject.name + " leans right");

    }
    public void leanBack()
    {
        Debug.Log(gameObject.name + " leans back");

    }
    public void leanFoward()
    {
        Debug.Log(gameObject.name + " leans foward");

    }
    public void doTricks()
    {
        Debug.Log(gameObject.name + " does a trick");

    }
    public void jumpOnce()
    {
        Debug.Log(gameObject.name + " leans left");

    }
}
