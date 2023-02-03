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
        // S_ItemDatabase.greenFlagItem[itemDatabasePlacement].itemGreenFlagImage = itemImage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player"+"Character")
        {

            if (tag == "RedFlag")
            {
                Debug.Log(name + " is a red flag");
                if (other.tag == "Character")
                {
                    Debug.Log(other.name + " should be hit with " + name);
                    //do specific effect
                }
                if (other.tag == "Player")
                {
                    Debug.Log(other.name + " should be hit with " + name);

                    //do specific effect

                }
            }
            if (tag == "GreenFlag")
            {
                Debug.Log(name + " is a green flag");
                if (other.gameObject.tag == "RedFlag")
                {
                    transform.LookAt(other.gameObject.transform);
                    Debug.Log(name + " should be facing " + other.name);
                    //do specific effect
                    itemEffectPrefab.GetComponent<S_Effect>().activateEffect(other.gameObject, gameObject);
                    Debug.Log(name + " should be playing " + itemEffectPrefab.name);


                }
                else
                {
                    Debug.Log(other.name + " isnt a red flag");
                }
            }
        }
        else
        {
            Debug.Log(other.name);
        }


    }
}
