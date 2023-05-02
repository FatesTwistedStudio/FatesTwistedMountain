using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ButtonSFX : MonoBehaviour
{
        [HideInInspector]
    public S_AudioManager manager;

    public void Start() 
    {
        manager = FindObjectOfType<S_AudioManager>(); 
    }

    public void HighlightSFX()
    {
        manager.Play("Button-Highlight");
    }

    public void ConfirmSFX()
    {
        manager.Play("Button-Confirm");
    }

    public void QuitSFX()
    {
        manager.Play("Button-Quit");
    }
    public void PauseSFX()
    {
        manager.Play("Button-Pause");
    }
    public void ResumeSFX()
    {
        manager.Play("Button-Resume");
    }
    public void OptionsSFX()
    {
        manager.Play("Button-Options");
    }
    public void PortraitConirm()
    {
        manager.Play("Button-CHR");
    }
}
