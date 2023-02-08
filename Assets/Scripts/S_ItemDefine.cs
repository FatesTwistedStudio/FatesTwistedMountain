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
            if (name == "The SidEffect")
            {
                SidEffect(characterUsedItem);
            }

            if (name == "The SfbEffect")
            {
                SfbEffect();
            }
            if (name == "The WIP")
            {
                WipEffect(characterUsedItem);
            }
        }
    }
    private void Start()
    {
        if (name == "The BfgEffect")
        {
            BfgEffect(characterUsedItem);
        }
        if (name == "The PiiEffect")
        {
            PiiEffect(characterUsedItem);
        }
        if (name == "The NIE")
        {
            NieEffect(characterUsedItem);
        }
        if (name == "The HOV")
        {
            HovEffect(characterUsedItem);
        }
        if (name == "The MpeEffect")
        {
            MpeEffect();
        }
        if (name == "The AbeEffect")
        {
            AbeEffect();
        }
        if (name == "The AsiEffect")
        {
            AsiEffect();
        }
        if (name == "The CcsEffect")
        {
            CcsEffect(characterUsedItem);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            if (name == "The GstEffect")
            {
                GstEffect(other.gameObject);
            }
        }
        if (other.tag == "Player")
        {
            if (name == "The GstEffect")
            {
                GstEffect(other.gameObject);
            }
        }
        if (other.gameObject.tag == "RedFlag")
        {
            if (name == "The EDS")
            {
                EdsEffect();
            }
        }

    }

    public void playEffect()
    {
        if (name == "The ICF")
        {
            S_Effect.ICF();
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

