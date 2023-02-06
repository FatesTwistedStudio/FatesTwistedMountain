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
    public void ABE()
    {
        //spawn fire effect
        GameObject activeAbeEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //touching the fire lows down character

        //spawn icepatch
        //Icepatch();
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
    public void GoldenFlagEffect(GameObject character)
    {
        // add speed multiplier based on number of goldflags

        //respawning loses all all flags

        // getting hit by redflag loses 1


    }
}
