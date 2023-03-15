using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CharInfoHolder : MonoBehaviour
{
    public float timedTrial;
    public int pointsEarned;
    public int numGoldFlags;
    public GameObject itemHeld;
    public Sprite itemSprite;
    public GameObject camFollowPoint;
    public string _name;
    public Sprite image;
    public Vector3 holdingPosition;
    public Vector3 holdingUp;
    [SerializeField]
    public int[] levelPlacement;
   


    // public bool isPlayer = false;
    private void Update()
    {
        gameObject.name = _name;
        if (itemHeld != null)
        {
            itemSprite = itemHeld.GetComponent<S_ItemDefine>().itemImage;

        }
        holdingPosition = transform.position + holdingUp;
    }

}
