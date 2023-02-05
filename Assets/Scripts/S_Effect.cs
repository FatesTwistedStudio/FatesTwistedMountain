using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class S_Effect : MonoBehaviour
{
    public GameObject effectToBePlayed;
    public Collider itemEffectCollider;
    // Start is called before the first frame update
    private void Update()
    {

        if (itemEffectCollider != null)
        {
            gameObject.GetComponent<Collider>().transform.position = itemEffectCollider.transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RedFlag")
        {
            //setActivateEffect(other.gameObject,gameObject);
        }
    }
    public void setActivateEffect(GameObject itemUsed, GameObject intrudingItem)
    {
        effectToBePlayed = itemUsed.GetComponent<S_ItemDefine>().itemEffectPrefab;
        Debug.Log(itemUsed.name + " is spawning an effect");
    }

    public void WIP()
    {
        // spawn glider 
        GameObject activeWipEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        // gives more airtime for tricks

        // last 3 times
    }

    public void PII()
    {
        //spawn canvas 
        GameObject activePiiEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        // snow splatters appear

        //snow splaters appearance, size and placement are randomized

        // lasts a couple seconds

    }
    public void ABE()
    {
        //spawn fire effect
        GameObject activeAbeEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //touching the fire lows down character

        //spawn icepatch
        Icepatch();
        // gain speed boost

        //lasts a couple seconds

    }
    public void ASI()
    {
        //spawn airhorn
        GameObject activeAsiEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //play one-shot audioclip

        //all players lose momentum
    }
    public void CCS()
    {
        //spawn antenna
        GameObject activeCcsEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //change controls

        //reset controls after a couple seconds

    }
    public void GST()
    {
        //spawn magic circle
        GameObject activeGstEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //players who touch prefab cant jump

        //lasts 5-10 secs

        //effect lasts couple seconds
    }
    public void ICF()
    {
        //spawn fake flag
        GameObject activeIcfEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

    }
    public void MPE()
    {
        //spawn mud puddle
        GameObject activeMpeEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //slow player who are touching prefab

        //remains for 10 seconds, then turns to icepatch

    }
    public void SFB()
    {
        //spawn drone
        GameObject activeSfbEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //pressing Q will launch fire prefab

        //icepatch appears whereever fire touches

        //players who touch fire are stunned

    }
    public void SID()
    {
        //spawn launcher and 3 shards
        GameObject activeSidEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //pressing Q again will shoot one shard and disable one from launcher

        //shard moves foward

        //if character enters collider,  look at and continue foward

        //if character collides with shard , stun character

    }
    public void Icepatch()
    {
        //spawn effect
        GameObject activeIcePatchEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

        //speed up player

        //slow down player input while on patch


    }

    public void GoldenFlagEffect(GameObject character)
    {
        // add speed multiplier based on number of goldflags

        //respawning loses all all flags

        // getting hit by redflag loses 1


    }
}
