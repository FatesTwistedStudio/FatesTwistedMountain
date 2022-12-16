using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class S_CanvasController : MonoBehaviour
{
    public Image background;
    public TextMeshProUGUI title;
    public Button quit;
    public Button playButton;
    public S_GameloopController S_GameloopController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        S_GameloopController = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>();
        if (playButton != null)
        {

            if (S_GameloopController.player == null)
            {
                playButton.gameObject.SetActive(false);
            }
            else { playButton.gameObject.SetActive(true); }
        }
    }

}
