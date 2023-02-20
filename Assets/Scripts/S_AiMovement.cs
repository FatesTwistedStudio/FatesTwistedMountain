using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class S_AiMovement : MonoBehaviour
{
    private GameObject EventControl;
    public NavMeshAgent aiCharacter;
    private Vector3 whereToGo;

    public float Speed;
    private void Start()
    {
        whereToGo = GameObject.FindWithTag("Finish").transform.position;
    }
    //focus on items
    private void Update()
    {
        EventControl = GameObject.FindWithTag("EventController");
        if(gameObject.tag == "Character")
        {
            aiCharacter.SetDestination(whereToGo);
        }
    }
    public void seeItem(GameObject item)
    {
        whereToGo=item.transform.position;
        Debug.Log(gameObject.name + " seees an item");
    }
    public void useItem(GameObject itemToUse)
    {
        EventControl.GetComponent<S_EventController>().useItem(itemToUse);
        Debug.Log(gameObject.name + " uses an item");
    }
    public void discardItem(GameObject itemToDiscard)
    {
        EventControl.GetComponent<S_EventController>().discardItem(itemToDiscard);
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
        //animation
        //
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
