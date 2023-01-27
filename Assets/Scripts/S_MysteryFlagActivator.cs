using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class S_MysteryFlagActivator : MonoBehaviour
{
    public int itemNum;
    public GameObject item;
    public Sprite itemMysteryFlagImage;
    public S_ItemDatabase S_ItemDatabase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<S_CharInfoHolder>() == true)
        {
            if (other.GetComponent<S_CharInfoHolder>().itemHeld == null)
            {
                setTheItem(other.gameObject);
            }
        }
    }
    public void randomizeTheItem()
    {
        if (gameObject.tag == "RedFlag")
        {
            itemNum = Random.Range(0, S_ItemDatabase.redFlagItem.Length - 1);
            item = S_ItemDatabase.redFlagItem[itemNum].itemPrefab;
            item.GetComponent<S_ItemDefine>().itemDatabasePlacement = itemNum;
            item.GetComponent<S_ItemDefine>().itemType = "RedFlag";

        }
        if (gameObject.tag == "GreenFlag")
        {
            itemNum = Random.Range(0, S_ItemDatabase.greenFlagItem.Length - 1);
            item = S_ItemDatabase.greenFlagItem[itemNum].itemGreenFlagPrefab;
            item.GetComponent<S_ItemDefine>().itemDatabasePlacement= itemNum;
            item.GetComponent<S_ItemDefine>().itemType = "GreenFlag";
        }
    }
    public void setTheItem(GameObject character)
    {
        if (character.GetComponent<S_CharInfoHolder>().itemHeld == null)
        {
            if (S_ItemDatabase != null)
            {
                if (item != null)
                {
                    character.GetComponent<S_CharInfoHolder>().itemHeld = item;
                    gameObject.SetActive(false);
                }
                else
                {
                    randomizeTheItem();
                }
            }
        }
    }
}

