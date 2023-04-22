using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SkyboxRando : MonoBehaviour
{

    public Material skyOne;
    public Material skyTwo;
    public Material skyThree;
    public Material skyFour;
    public Material skyFive;

  

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skyOne;
        
       // skyBoxMaterial = materials[Random.Range(5, materials.length)];
        //RenderSettings.skybox = skyBoxMaterial

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
