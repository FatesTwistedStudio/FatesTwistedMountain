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
    public S_Effect S_Effect;
    public int pointWorth;
    private void OnTriggerStay(Collider other)
    {
        if (gameObject.name == "The EdsEffect")
        {
            if (other.gameObject.tag == "RedFlag")
            {
                //look at redflagitem
                transform.LookAt(other.gameObject.transform);
            }
        }
    }
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //fire extra projectiles

        }
    }
    private void Start()
    {
        if (name == "The BFG")
        {
            BFG(characterUsedItem);
        }
        if (name == "The HOV")
        {
            HovEffect(characterUsedItem);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //item being used is a red flag
        if (tag == "RedFlag")
        {
            if (other.gameObject != characterUsedItem)
            {
                if (willChase == false)
                {

                }
                if (willChase == true)
                {

                }
                //target aquisition for red flag
                if (other.tag == "Character")
                {
                    itemEffectPrefab.GetComponent<S_Effect>().setActivateEffect(other.gameObject, gameObject);

                    //do specific effect
                    Debug.Log(other.name + " should be hit with " + name);
                }
                if (other.tag == "Player")
                {

                    //do specific effect
                    Debug.Log(other.name + " should be hit with " + name);
                }
            }
        }
        if (tag == "GreenFlag")
        {
            playEffect();
        }
        if (name == "The EDS")
        {
            if(other.gameObject.tag =="RedFlag")
            {
                EdsEffect(other.gameObject);
            }
        }

    }

    public void playEffect()
    {
        if (name == "The NIE")
        {
            //S_Effect.NieEffect();
        }
        if (name == "The WIP")
        {
            S_Effect.WIP();
        }
        if (name == "The ABE")
        {
            S_Effect.ABE();
        }
        if (name == "The ASI")
        {
            S_Effect.ASI();
        }
        if (name == "The CCS")
        {
            S_Effect.CCS();
        }
        if (name == "The GST")
        {
            S_Effect.GST();
        }
        if (name == "The ICF")
        {
            S_Effect.ICF();
        }
        if (name == "The MPE")
        {
            S_Effect.MPE();
        }
        if (name == "The PII")
        {
            S_Effect.PII();
        }
        if (name == "The SFB")
        {
            S_Effect.SFB();
        }
        if (name == "The SID")
        {
            S_Effect.SID();
        }
        //add points

    }
    public void BFG(GameObject CharacterToEffect)
    {
        itemEffectPrefab.GetComponent<S_BfgEffect>().character = CharacterToEffect;
        GameObject activeBfgEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;
       
        if (activeBfgEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void EdsEffect(GameObject itemToDestroy)
    {
        //shoots a laser
        GameObject activeEdsLaserEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeEdsLaserEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void HovEffect(GameObject CharacterToEffect)
    {
        //effect should look like pulsing waves under the board
        GameObject activeHovEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeHovEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void NieEffect(GameObject CharacterToEffect)
    {
        //activate headphones
        GameObject activeNieEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeNieEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
}

