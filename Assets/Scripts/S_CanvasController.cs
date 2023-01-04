using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class S_CanvasController : MonoBehaviour
{
    public GameObject levelSelect;
    public S_GameloopController S_GameloopController;
    // Start is called before the first frame update
    private void Update()
    {

        if (S_GameloopController != null)
        {
            if (S_GameloopController.player != null)
            {
                levelSelect.SetActive(true);
            }
            else
            {
                levelSelect.SetActive(false);
            }
        }
    }  

}
