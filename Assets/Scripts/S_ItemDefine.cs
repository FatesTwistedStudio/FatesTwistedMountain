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
    public int pointWorth;

    [SerializeField] private Item _item;

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.name == "The EDS")
        {
            if (other.gameObject.tag == "RedFlag")
            {
                //look at redflagitem
                Debug.Log("EDS looking at " + other.gameObject.name);
                transform.LookAt(other.gameObject.transform);
            }
        }
    }

    //enum/id of items
    public enum Item
    {
        BFG,
        NIE,
        EDS,
        HOV,
        MPE,
        ABE,
        ASI,
        PII,
        SFB,
        CCS,
        GST,
        SID,
        WIP
    };

    Item itemChosen;

    private void Start()
    {
        Debug.Log("Name " + name);

    }

    private void Update()
    {
        if (willChase == true)
        {
            //apply physics to move foward
            Debug.Log("item launch forward");
        }
        if (willFollow == true)
        {
            Debug.Log("item is following");

            gameObject.transform.position = characterUsedItem.GetComponent<S_CharInfoHolder>().holdingPosition;
            //set to follow above player
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //fire extra projectiles
            if (name == "The SID")
            {
                Debug.Log("Sid effect item launch forward");
                SidEffect(characterUsedItem);
            }
            if (name == "The WIP")
            {
                Debug.Log("Wipeffect");
                WipEffect(characterUsedItem);
            }
        

        itemChosen = Item.BFG;

          /* Code for switch for item randomizer 
            switch (itemChosen)
         {
            case Item.BFG:
             Debug.Log("Spawning BfgEffect");
             //BfgEffect(characterUsedItem);
             break;
            
            case Item.NIE:
             Debug.Log("Spawning NieEffect");
             //NieEffect(characterUsedItem);
             break;

            case Item.EDS:
             Debug.Log("EDS Spawn");
             //EdsEffect();
             break;

            case Item.HOV:
             Debug.Log("Spawning HovEffect");
             //HovEffect(characterUsedItem);
             break;

            case Item.MPE:
             Debug.Log("Spawning MpeEffect");
             //MpeEffect();
             break;

            case Item.ABE:
             Debug.Log("Spawning AbeEffect");
             //AbeEffect();
             break;

            case Item.ASI:
             Debug.Log("Spawning AsiEffect");
             //AsiEffect();
             break;

            case Item.PII:
             Debug.Log("Spawning PiiEffect");
             //PiiEffect(characterUsedItem);
             break;

            case Item.SFB:
             Debug.Log("SFB effect item leaves flames behind ");
             //SfbEffect();
             break;

            case Item.CCS:
             Debug.Log("Spawning CcsEffect");
             //CcsEffect(characterUsedItem);
             break;

            default:
             Debug.Log("No Item");
             break;
          }
            */
        }

        //code to perform item power 
        if (_item == Item.BFG)
        {
            Debug.Log("Spawning BfgEffect");
            BfgEffect(characterUsedItem);
        }
        else if (_item == Item.PII)
        {
            Debug.Log("Spawning PiiEffect");
            PiiEffect(characterUsedItem);
        }
        else if (_item == Item.NIE)
        {
            Debug.Log("Spawning NieEffect");
            NieEffect(characterUsedItem);
        }
        else if (_item == Item.HOV)
        {
            Debug.Log("Spawning HovEffect");
            HovEffect(characterUsedItem);

        }
        else if (_item == Item.SFB)
        {
            Debug.Log("SFB effect item leaves flames behind ");
            SfbEffect();
        }
        else if (_item == Item.MPE)
        {
            Debug.Log("Spawning MpeEffect");
            MpeEffect();
        }
        else if (_item == Item.ABE)
        {
            Debug.Log("Spawning AbeEffect");
            AbeEffect();
        }
        else if (_item == Item.ASI)
        {
            Debug.Log("Spawning AsiEffect");
            AsiEffect();
        }
        else if (_item == Item.CCS)
        {
            Debug.Log("Spawning CcsEffect");
            CcsEffect(characterUsedItem);
        }
        else if (_item == Item.GST)
        {
            Debug.Log("Spawning GstEffect");
            GstEffect(characterUsedItem);
        }
        else if (_item == Item.SID)
        {
            Debug.Log("Spawning SidEffect");
            SidEffect(characterUsedItem);
        }
        else if (_item == Item.WIP)
        {
            Debug.Log("Spawning WipEffect");
            WipEffect(characterUsedItem);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            if (name == "The GST")
            {
                Debug.Log("Spawning GstEffect");

                GstEffect(other.gameObject);
            }
        }
        if (other.tag == "Player")
        {
            if (name == "The GST")
            {
                Debug.Log("Spawning GstEffect");

                GstEffect(other.gameObject);
            }
        }
        if (other.gameObject.tag == "RedFlag")
        {
            if (name == "The EDS")
            {
                Debug.Log("EDS Spawn");
                EdsEffect();
 
            }
        }

    }

    public void playEffect()
    {
        if (name == "The ICF")
        {
            //S_Effect.ICF();
        }
    }
    public void BfgEffect(GameObject CharacterToEffect)
    {
        itemEffectPrefab.GetComponent<S_BfgEffect>().character = CharacterToEffect;
        GameObject activeBfgEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeBfgEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void EdsEffect()
    {
        //shoots a laser
        GameObject activeEdsLaserEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeEdsLaserEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void SidEffect(GameObject CharacterToAvoid)
    {
        itemEffectPrefab.GetComponent<S_SidEffect>().character = CharacterToAvoid;
        int Ammo = 3;
        //spawn launcher and 3 shards
        GameObject activeSidEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        //pressing Q again will shoot one shard and disable one from launcher
        if (activeSidEffect.IsDestroyed())
        {
            Ammo--;
        }
        if (Ammo <= 0)
        {
            Destroy(gameObject);

        }
    }
    public void PiiEffect(GameObject CharacterToAvoid)
    {
        itemEffectPrefab.GetComponent<S_PiiEffect>().character = CharacterToAvoid;
        //spawn canvas 
        GameObject activePiiEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activePiiEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void NieEffect(GameObject CharacterToEffect)
    {
        itemEffectPrefab.GetComponent<S_NieEffect>().character = CharacterToEffect;
        //activate headphones
        GameObject activeNieEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeNieEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void HovEffect(GameObject CharacterToEffect)
    {
        itemEffectPrefab.GetComponent<S_HovEffect>().character = CharacterToEffect;
        //effect should look like pulsing waves under the board
        GameObject activeHovEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeHovEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void MpeEffect()
    {
        //spawn mud puddle
        GameObject activeMpeEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeMpeEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void SfbEffect()
    {
        //spawn drone
        GameObject activeSfbEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeSfbEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void AbeEffect()
    {
        itemEffectPrefab.GetComponent<S_AbeEffect>().character = characterUsedItem;

        //spawn fire effect
        GameObject activeAbeEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeAbeEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void AsiEffect()
    {
        itemEffectPrefab.GetComponent<S_AsiEffect>().character = characterUsedItem;

        //spawn airhorn
        GameObject activeAsiEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeAsiEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void WipEffect(GameObject CharacterToEffect)
    {
        itemEffectPrefab.GetComponent<S_WipEffect>().character = CharacterToEffect;
        int Ammo = 3;
        // spawn glider 
        GameObject activeWipEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;
        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeWipEffect.IsDestroyed())
        {
            Ammo--;
        }
        if (Ammo <= 0)
        {
            Destroy(gameObject);

        }
    }
    public void CcsEffect(GameObject playerWhoIsControlling)
    {
        itemEffectPrefab.GetComponent<S_CcsEffect>().character = playerWhoIsControlling;

        //spawn antenna
        GameObject activeCcsEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;

        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeCcsEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
    public void GstEffect(GameObject CharacterToEffect)
    {
        itemEffectPrefab.GetComponent<S_GstEffect>().character = CharacterToEffect;

        //spawn magic circle
        GameObject activeGstEffect = Instantiate(itemEffectPrefab, transform.position, transform.rotation) as GameObject;

        characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned = characterUsedItem.GetComponent<S_CharInfoHolder>().pointsEarned + pointWorth;

        if (activeGstEffect.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
}

