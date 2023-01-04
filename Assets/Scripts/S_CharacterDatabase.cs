using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterDatabase : MonoBehaviour
{
    [System.Serializable]
    public class CharacterInfo
    {
        public string characterName;
        public Sprite characterImage;
        public GameObject characterPrefab;
    }
    public CharacterInfo[] characterInformation = new CharacterInfo[1];
}
