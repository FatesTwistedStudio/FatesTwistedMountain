using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckpointReceiver : MonoBehaviour
{
    
    [SerializeField]
    public float CheckpointTime;

    
    private S_HUD HUD;
    private float HUDTime;

    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
         col = GetComponent<Collider>();
        HUD = FindObjectOfType<S_HUD>();
    }

    void Update()
    {
        HUDTime = HUD.ingameTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Checkpoint");
        col.enabled = false;
        ShowDelta();
        Debug.Log(HUDTime);

    }

    private void ShowDelta()
    {
        float delta = 0;
        delta = HUDTime - CheckpointTime;
        HUD.deltaTime = delta;
        Debug.Log(delta);
        //if ()
    }
}