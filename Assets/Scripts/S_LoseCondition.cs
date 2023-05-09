using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LoseCondition : MonoBehaviour
{
    S_HUD hud;
    float time;
    bool timeWarning = false;
    bool timeisUp = false;
    public S_PlayerInput playerRef;
    public S_StopMusic endAudio;
    public S_Transition transiton;
    public Animator anim;
    S_HoverboardPhysic physics;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        hud = FindObjectOfType<S_HUD>();
    }

    // Update is called once per frame
    void Update()
    {
        time = hud.ingameTime;
        playerRef = FindObjectOfType<S_PlayerInput>();
        physics = FindObjectOfType<S_HoverboardPhysic>();
        rb = physics.gameObject.GetComponent<Rigidbody>();

        if (time > 150 && !timeWarning)
        {
            timeWarning = true;
            Debug.Log("Warning");
            anim.Play("a_LC_Warning");
        }

        if (time > 300 && !timeisUp && timeWarning)
        {
            timeisUp = true;
            Debug.Log("You lose");
            anim.Play("a_LC_Lose");
            playerRef.Lose();
            playerRef.enabled = false;
            physics.enabled = false;
            hud.gameObject.SetActive(false);
            physics.baseVelocity = 0;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void Quit()
    {
        endAudio = FindObjectOfType<S_StopMusic>();
        endAudio.StopAudio();
        endAudio.StopMusic();

        Time.timeScale = 1;
        AudioListener.pause = false;
        transiton.loadScene("MainMenu");
    }
}
