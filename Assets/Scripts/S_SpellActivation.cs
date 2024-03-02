using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SpellActivation : MonoBehaviour
{
    public SpellData spellData;

    public GameObject caster;

    private void Awake()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {

            if (spellData.willChase)
            {
                followingRules(other.gameObject);
            }

            if (spellData.willFollow)
            {
                followingRules(caster);
            }
        }
    }
    public void followingRules(GameObject CharToFollow)
    {
        SpawnSpell();
    }

    public void SpawnSpell()
    {
        if (caster != null)
        {
            //Instantiate(SpellData.spellEffectPrefab);

        }
    }
}
