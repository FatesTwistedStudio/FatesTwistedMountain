using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class S_ChickenSwarm : MonoBehaviour
{
    public Animator[] toonChickens;
    public bool isTurningHead;

    private void Awake()
    {
        //toonChickens = GetComponentsInChildren<Animator>();
        //toonChickens[0].GetComponent<AnimatorController>()
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isTurningHead)
        {
           // toonChickens[0]
        }
    }
}
