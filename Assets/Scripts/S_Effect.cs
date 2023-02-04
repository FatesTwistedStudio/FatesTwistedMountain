using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class S_Effect : MonoBehaviour
{
    public GameObject EdsLaserEffect;
    public GameObject NieHeadphonesEffect;
    public GameObject IcePatchEffect;
    public Collider itemEffectCollider;
    // Start is called before the first frame update
    private void Update()
    {

        if(itemEffectCollider!=null)
        {
            gameObject.GetComponent<Collider>().transform.position = itemEffectCollider.transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="RedFlag")
        {

            other.gameObject.SetActive(false);
        }
    }
    public void setActivateEffect(GameObject incomingItem, GameObject itemUsed)
    {
        Debug.Log(itemUsed.name +" is spawning an effect");
        if (itemUsed.name == "The EDS")
        {
            EdsLaser(incomingItem);
        }
        if (itemUsed.name == "The NIE")
        {
            EdsLaser(incomingItem);
        }
        if (itemUsed.name == "The WIP")
        {
            EdsLaser(incomingItem);
        }
        if (itemUsed.name == "The HOV")
        {
            EdsLaser(incomingItem);
        }
        if (itemUsed.name == "The BFG")
        {
            EdsLaser(incomingItem);
        }
    }
   public void EdsLaser(GameObject itemToDestroy)
    {
        GameObject activeEdsLaserEffect = Instantiate(EdsLaserEffect, transform.position, transform.rotation) as GameObject;
        itemEffectCollider = activeEdsLaserEffect.GetComponent<Collider>();
        Debug.Log("Play Laser effect");
    }
    public void NieHeadphones()
    {
        //activate headphones-they should play music and enhance player accelleration for couple seconds
        GameObject activeNieEffect = Instantiate(NieHeadphonesEffect, transform.position, transform.rotation) as GameObject;
    }
    public void Icepatch()
    {
        //spawn effect
        GameObject activeNieEffect = Instantiate(IcePatchEffect, transform.position, transform.rotation) as GameObject;

        //speed up player
        //slow down player input

    }
    public void BFG()
    {

    }
    public void HOV()
    {

    }
    public void WIP()
    {

    }
}
