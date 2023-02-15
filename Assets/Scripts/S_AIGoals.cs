using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AIGoals : MonoBehaviour
{
    public string aiText;
    public S_AiMovement S_AiMovement;

    private void Update()
    {
        if (gameObject.tag == "Character")
        {
            if (aiText == "")
            {
                AiWilson();
            }
            if (aiText == "Slime")
            {
                AiSlime();
            }
            if (aiText == "Larry")
            {
                AiLarry();
            }
            if (aiText == "JeroyLenkins")
            {
                AiJeroyLenkins();
            }
            if (aiText == "RickyBobby")
            {
                AiRickyBobby();
            }
        }
    }
    public void AiSlime()
    {
        // easiest

        // will go for red and green flags and use them
        if (gameObject.GetComponent<S_CharInfoHolder>() != null)
        {
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "RedFlag")
            {
                S_AiMovement.useItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "GreenFlag")
            {
                S_AiMovement.useItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);
            }
        }
        // eager for points will use every oporunioty for it(Ie doing tricks and items at the cost of speed and time)

        //lower mass
    }
    public void AiLarry()
    {
        //medium

        //avoid red and green flags
        if (gameObject.GetComponent<S_CharInfoHolder>() != null)
        {
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "RedFlag")
            {
                S_AiMovement.discardItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "GreenFlag")
            {
                S_AiMovement.discardItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
        }
        //not serious about points(not going to do tricks)

        //just get to the goal
    }
    public void AiJeroyLenkins()
    {
        //hard

        //avoid red flags but will use green flags
        if (gameObject.GetComponent<S_CharInfoHolder>() != null)
        {
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "RedFlag")
            {
                S_AiMovement.discardItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "GreenFlag")
            {
                S_AiMovement.useItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
        }
        //will get point where it can(ie doing tricks)

        //fastest
    }
    public void AiRickyBobby()
    {
        //expert

        //Will use red flag items and avoid green flag items
        if (gameObject.GetComponent<S_CharInfoHolder>() != null)
        {
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "RedFlag")
            {
                S_AiMovement.useItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "GreenFlag")
            {
                S_AiMovement.discardItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
        }
        //will do tricks

        //will try to make the player lose using items like a menace
    }
    public void AiWilson()
    {
        //unreal

        //Will use red flag and green flag items
        if (gameObject.GetComponent<S_CharInfoHolder>() != null)
        {
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "RedFlag")
            {
                S_AiMovement.useItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
            if (gameObject.GetComponent<S_CharInfoHolder>().itemHeld.tag == "GreenFlag")
            {
                S_AiMovement.useItem(gameObject.GetComponent<S_CharInfoHolder>().itemHeld);

            }
        }
        //will do tricks
        
        //will try to make the player lose using items like a menace
    }
}
