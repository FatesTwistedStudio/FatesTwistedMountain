using System.Collections;
using System.Collections.Generic;
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
            //find target
            //apply physics to chase

        }
        if (willFollow == true)
        {
            //set to follow above player
        }
        // S_ItemDatabase.greenFlagItem[itemDatabasePlacement].itemGreenFlagImage = itemImage;
    }
    private void Awake()
    {
        Debug.Log(gameObject.name + " is awake");

    }
    public void playEffect()
    {
        GameObject itemEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        Debug.Log("item effect");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (tag == "RedFlag")
        {
            if (other.tag == "Character")
            {
                //do specific effect
                playEffect();
            }
            if (other.tag == "Player")
            {
                //do specific effect

            }
        }
        if (tag == "GreenFlag")
        {
            if (other.tag == "Character")
            {
                //do specific effect
                playEffect();
            }
            if (other.tag == "Player")
            {
                //do specific effect

            }
        }
    }


}
