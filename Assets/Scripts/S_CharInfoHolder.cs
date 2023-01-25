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
    public GameObject camFollowPoint;
    public string _name;
    public Sprite image;
    // public bool isPlayer = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            pointsEarned++;
        }
        gameObject.name= _name;
    }
    
}
