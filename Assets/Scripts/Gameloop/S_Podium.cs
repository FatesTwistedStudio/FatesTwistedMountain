using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Podium : MonoBehaviour
{
    public GameObject player;
    public GameObject gameController;
    public GameObject podium1st;
    public GameObject podium2st;
    public GameObject podium3st;
    public GameObject podium4st;
    public S_Credits S_Credits;
    private void Awake()
    {
        if (gameController == null)
            gameController = GameObject.FindWithTag("GameController");

        if (player == null)
            player = gameController.GetComponent<S_GameloopController>().player;

        if (player != null)
            spawnPlayer();
    }
    private void Update()
    {
        if (GameObject.FindWithTag("EventController") == true)
            GameObject.FindWithTag("EventController").gameObject.SetActive(false);

        player = gameController.GetComponent<S_GameloopController>().player;
       // Debug.Log(player.GetComponent<S_CharInfoHolder>().levelPlacement[0]);
    }
    public void spawnPlayer()
    {
        if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 1)
        {
            GameObject spawnCharacter = Instantiate(player, podium1st.transform.position, podium1st.transform.rotation) as GameObject;
            if (spawnCharacter.GetComponent<Rigidbody>() != null)
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
    //private void OnDestroy()
    //{
    //    GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>().player.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 0;
    //    Destroy(GameObject.FindWithTag("GameController"));
    //    Destroy(gameObject);

    //}
}
