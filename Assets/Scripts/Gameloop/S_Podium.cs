using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Podium : MonoBehaviour
{
    public GameObject player;
    public GameObject podium1st;
    public GameObject podium2st;
    public GameObject podium3st;
    public GameObject podium4st;
    public S_Credits S_Credits;
    private void Awake()
    {
        if (player == null)
            player = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>().player;



        if (player != null)
        {
            spawnPlayer();

        }
    }
    private void Update()
    {
        Debug.Log(player.GetComponent<S_CharInfoHolder>().levelPlacement[0]);
        GameObject.FindWithTag("EventController").gameObject.SetActive(false);

    }
    public void spawnPlayer()
    {
        Debug.Log("player present");
        if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 1)
        {
            GameObject spawnCharacter = Instantiate(player, podium1st.transform.position, podium1st.transform.rotation) as GameObject;
            spawnCharacter.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            spawnCharacter.GetComponent<S_HoverboardPhysic>().enabled = false;
            spawnCharacter.GetComponent<S_SurfaceAlignment>().enabled = false;
            spawnCharacter.GetComponent<S_HandleCinemachine>().enabled = false;
            spawnCharacter.GetComponent<PlayerInput>().enabled = false;
            spawnCharacter.GetComponent<S_Stun>().enabled = false;
        }
        else if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 2)
        {
            GameObject spawnCharacter = Instantiate(player, podium2st.transform.position, podium2st.transform.rotation) as GameObject;
            spawnCharacter.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            spawnCharacter.GetComponent<S_HoverboardPhysic>().enabled = false;
            spawnCharacter.GetComponent<S_SurfaceAlignment>().enabled = false;
            spawnCharacter.GetComponent<S_HandleCinemachine>().enabled = false;
            spawnCharacter.GetComponent<PlayerInput>().enabled = false;
            spawnCharacter.GetComponent<S_Stun>().enabled = false;

        }
        else if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 3)
        {
            GameObject spawnCharacter = Instantiate(player, podium3st.transform.position, podium3st.transform.rotation) as GameObject;
            spawnCharacter.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            spawnCharacter.GetComponent<S_HoverboardPhysic>().enabled = false;
            spawnCharacter.GetComponent<S_SurfaceAlignment>().enabled = false;
            spawnCharacter.GetComponent<S_HandleCinemachine>().enabled = false;
            spawnCharacter.GetComponent<PlayerInput>().enabled = false;
            spawnCharacter.GetComponent<S_Stun>().enabled = false;

        }
        else
        {
            GameObject spawnCharacter = Instantiate(player, podium4st.transform.position, podium4st.transform.rotation) as GameObject;
            spawnCharacter.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            spawnCharacter.GetComponent<S_HoverboardPhysic>().enabled = false;
            spawnCharacter.GetComponent<S_SurfaceAlignment>().enabled = false;
            spawnCharacter.GetComponent<S_HandleCinemachine>().enabled = false;
            spawnCharacter.GetComponent<PlayerInput>().enabled = false;
            spawnCharacter.GetComponent<S_Stun>().enabled = false;

        }


    }

}
