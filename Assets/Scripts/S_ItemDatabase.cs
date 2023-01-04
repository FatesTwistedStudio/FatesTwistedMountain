using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ItemDatabase : MonoBehaviour
{
    [System.Serializable]
    public class GreenFlag
    {
        public string itemName;
        
        public Sprite itemImage;
        public GameObject itemPrefab;
        public int pointsGiven;
    }
    public GreenFlag[] greenFlagItem = new GreenFlag[3];

    [System.Serializable]
    public class RedFlag
    {
        public string itemName;
      
        public Sprite itemImage;
        public GameObject itemPrefab;
        public int pointsGiven;
    }
    public RedFlag[] redFlagItem = new RedFlag[3];
    
    [System.Serializable]
    public class GoldFlag
    {
        public string itemName;
        public Sprite itemImage;
        public GameObject itemPrefab;
        public int pointsGiven;
    }
    public GoldFlag[] goldFlagItem = new GoldFlag[1];

}
