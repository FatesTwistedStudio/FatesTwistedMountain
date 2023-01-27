using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ItemDefine : MonoBehaviour
{
    public Sprite itemImage;
    public GameObject itemEffectPrefab;
    public string itemType;
    public int itemDatabasePlacement;
    public Vector3 holdingPosition;
    public S_ItemDatabase S_ItemDatabase;
    private void Update()
    {
       // S_ItemDatabase.greenFlagItem[itemDatabasePlacement].itemGreenFlagImage = itemImage;
    }
    public void playEffect()
    {
        Instantiate(itemEffectPrefab);
    }


}
