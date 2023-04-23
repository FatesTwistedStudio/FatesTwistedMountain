using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public class S_SkyboxRando : MonoBehaviour
{

    //public Material skyOne;
    //public Material skyTwo;
    //public Material skyThree;
    //public Material skyFour;
    //public Material skyFive;

    //[SerializeField]

    public Material[] skyBoxes;
     Material skyboxMaterial;
    // Start is called before the first frame update
    void Start()
    {

        skyboxMaterial = skyBoxes[Random.Range(0, skyBoxes.Length)];
        RenderSettings.skybox = skyboxMaterial;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
