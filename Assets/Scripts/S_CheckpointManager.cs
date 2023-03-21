using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class S_CheckpointManager : MonoBehaviour
{
    public float[] CheckpointTimes;
    public S_CheckpointReceiver[] Receivers;

    // Start is called before the first frame update
    void Start()
    {
       foreach (S_CheckpointReceiver receiver in Receivers)
        {
//            receiver.CheckpointTime = CheckpointTimes[i];
  //          Debug.Log("Set Checkpoint");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
