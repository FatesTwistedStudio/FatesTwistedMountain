using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_ItemDefine : MonoBehaviour
{
    public Sprite itemImage;
    public GameObject itemEffectPrefab;
    public string itemType;
    public int itemDatabasePlacement;
    public S_ItemDatabase S_ItemDatabase;
    public GameObject characterUsedItem;
    public bool willFollow;
    public bool willChase;

    private void Update()
    {
        if (willChase == true)
        {
            //apply physics to move foward
        }
        if (willFollow == true)
        {
            gameObject.transform.position = characterUsedItem.GetComponent<S_CharInfoHolder>().holdingPosition;
            //set to follow above player
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //item being used is a red flag
        if (tag == "RedFlag")
        {
            //target aquisition for red flag
            if (other.tag == "Character")
            {
                //do specific effect
                Debug.Log(other.name + " should be hit with " + name);
            }
            if (other.tag == "Player")
            {
                //do specific effect
                Debug.Log(other.name + " should be hit with " + name);
            }
        }
        if (gameObject.name == "The EDS")
        {
            //find redflagitems
            if (other.gameObject.GetComponent<S_ItemDefine>().itemType == "RedFlag")
            {
                //do specific effect
                itemEffectPrefab.GetComponent<S_Effect>().EdsLaser(other.gameObject);
        
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if(other.tag == "RedFlag")
        {
            //look at redflagitem
            transform.LookAt(other.gameObject.transform);
            Debug.Log(name + " should be facing " + other.name);
        }
    }
}
