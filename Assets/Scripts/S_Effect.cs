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

            other.gameObject.SetActive(false);
        }
    }
    public void setActivateEffect(GameObject incomingItem, GameObject itemUsed)
    {
        effectToBePlayed = itemUsed.GetComponent<S_ItemDefine>().itemEffectPrefab;
        Debug.Log(itemUsed.name + " is spawning an effect");
        if (itemUsed.name == "The EDS")
        {
            EdsLaser(incomingItem);
        }
        if (itemUsed.name == "The NIE")
        {
            NieEffect();
        }
        if (itemUsed.name == "The WIP")
        {
            WIP();
        }
        if (itemUsed.name == "The HovEffect")
        {
            HovEffect();
        }
        if (itemUsed.name == "The BFG")
        {
            BFG();
        }
        if (itemUsed.name == "The ABE")
        {
            ABE();
        }
        if (itemUsed.name == "The ASI")
        {
            ASI();
        }
        if (itemUsed.name == "The CCS")
        {
            CCS();
        }
        if (itemUsed.name == "The GST")
        {
            GST();
        }
        if (itemUsed.name == "The ICF")
        {
            ICF();
        }
        if (itemUsed.name == "The MPE")
        {
            MPE();
        }
        if (itemUsed.name == "The PII")
        {
            PII();
        }
        if (itemUsed.name == "The SFB")
        {
            SFB();
        }
        if (itemUsed.name == "The SID")
        {
            SID();
        }
    }
    public void HovEffect()
    {
        //effect should look like pulsing waves under the board
        GameObject activeHovEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        // avoid ground based effects

        // less mass

        //lasts a couple seconds

    }
    public void NieEffect()
    {
        //activate headphones
        GameObject activeNieEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //they should play music

        //enhance player accelleration 

        //lasts for couple seconds
    }
    public void EdsLaser(GameObject itemToDestroy)
    {
        Debug.Log("Play Laser effect");
        //shoots a laser
        GameObject activeEdsLaserEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        itemEffectCollider = activeEdsLaserEffect.GetComponent<Collider>();
        //laser defeats all projectile items

        // lasts 1 time
    }
    public void WIP()
    {
        // spawn glider 
        GameObject activeWipEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        // gives more airtime for tricks

        // last 3 times
    }
    public void BFG()
    {
        //spawn particle effect 
        GameObject activeBfgEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;
        //change player color
        
        //ignore all effects

        //speed up character

        //for random amount of time
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
        GameObject activeCcsEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

    }
    public void GST()
    {
        GameObject activeGstEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

    }
    public void ICF()
    {
        GameObject activeIcfEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

    }
    public void MPE()
    {
        GameObject activeMpeEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

    }
    public void SFB()
    {
        GameObject activeSfbEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

    }
    public void SID()
    {
        GameObject activeSidEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

    }
    public void Icepatch()
    {
        //spawn effect
        GameObject activeIcePatchEffect = Instantiate(effectToBePlayed, transform.position, transform.rotation) as GameObject;

        //speed up player

        //slow down player input while on patch

    }
}
