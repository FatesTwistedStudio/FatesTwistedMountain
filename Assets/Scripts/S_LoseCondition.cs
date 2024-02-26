using System.Collections.Generic;
using UnityEngine;

public class S_LoseCondition : MonoBehaviour
{
    [SerializeField] private S_HUD hud;
    [SerializeField] private S_StopMusic endAudio;
    [SerializeField] private S_Transition transiton;
    [SerializeField] private Animator anim;
    [SerializeField] private S_PauseMenu pauseMenu;
    [SerializeField] private S_PlayerInput playerRef;
    [SerializeField] private S_HoverboardPhysic physics;
    [SerializeField] private Rigidbody rb;

    private float timeWarningThreshold = 150f;
    private float timeOutThreshold = 300f;
    private bool isTimeWarningShown;
    private bool isTimedOut;
    private bool hasFoundPlayerRef;
    private bool hasFoundPhysics;

    void Update()
    {
        //Obtain references
        if (!hasFoundPlayerRef)
        {
            playerRef = FindObjectOfType<S_PlayerInput>();
            hasFoundPlayerRef = true;
        }
        if (!hasFoundPhysics)
        {
            physics = FindObjectOfType<S_HoverboardPhysic>();
            hasFoundPhysics = true;
            rb = physics.GetComponent<Rigidbody>();
        }

        if (hud.ingameTime > timeWarningThreshold && !isTimeWarningShown)
        {
            isTimeWarningShown = true;
            Debug.Log("Warning");
            anim.Play("a_LC_Warning");
        }

        if (hud.ingameTime > timeOutThreshold && !isTimedOut && isTimeWarningShown)
        {
            isTimedOut = true;
            Debug.Log("You lose");
            anim.Play("a_LC_Lose");
            pauseMenu.canPause = false;
            playerRef.Lose();
            playerRef.enabled = false;
            physics.enabled = false;
            hud.gameObject.SetActive(false);
            physics.baseVelocity = 0f;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void Quit()
    {
        endAudio?.StopAudio(); // Use null-conditional operator for potential null reference
        Time.timeScale = 1;
        AudioListener.pause = false;
        transiton.loadScene(0);
    }
}
