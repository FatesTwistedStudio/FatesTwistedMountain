using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class S_MysteryFlagActivator : MonoBehaviour
{
    public int itemNum;
    public GameObject item;
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

        if (S_ItemDatabase != null)
        {
            randomizeTheItem();
        }
        else
        { Debug.Log("no item database"); }
    }
    public void randomizeTheItem()
    {
        if (gameObject.tag == "RedFlag")
        {
            itemNum = Random.Range(0, S_ItemDatabase.redFlagItem.Length - 1);
        }
        if (gameObject.tag == "GreenFlag")
        {
            itemNum = Random.Range(0, S_ItemDatabase.greenFlagItem.Length - 1);
        }
    }
    public void setTheItem(GameObject character)
    {
        if (character.GetComponent<S_CharInfoHolder>().itemHeld == null)
        {
            if (gameObject.tag == "RedFlag")
            {
                if (S_ItemDatabase.redFlagItem[itemNum] != null)
                {
                    item = S_ItemDatabase.redFlagItem[itemNum].itemPrefab;

                }
                
            }
            if (gameObject.tag == "GreenFlag")
            {
                if (S_ItemDatabase.greenFlagItem[itemNum] != null)
                {
                    item = S_ItemDatabase.greenFlagItem[itemNum].itemPrefab;

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<S_CharInfoHolder>() == true)
        {
            if (other.GetComponent<S_CharInfoHolder>().itemHeld == null)
            {
                if (item != null)
                {
                    setTheItem(other.gameObject);
                    Debug.Log(other.GetComponent<S_CharInfoHolder>()._name + " has gotten " + other.GetComponent<S_CharInfoHolder>().itemHeld);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
