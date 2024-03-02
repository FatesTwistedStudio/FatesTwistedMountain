using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class SpellData : ScriptableObject
{
    public TypeOfSpells typeOfSpells;

    public string spellName;
    public Sprite spellImage;
    public GameObject spellEffectPrefab;

    public bool willFollow;
    public bool willChase;

    public int pointsGiven;

}
public enum TypeOfSpells { Defensive, Offensive, Support };
