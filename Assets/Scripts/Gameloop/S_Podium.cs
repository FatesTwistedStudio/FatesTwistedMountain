using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Podium : MonoBehaviour
{
    public GameObject player;
    public GameObject podium1st;
    public GameObject podium2st;
    public GameObject podium3st;
    public S_Credits S_Credits;
    private void Update()
    {
        if (player == null)
            player = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>().player;


        if (player != null)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                spawnPlayer(player, player.GetComponent<S_CharInfoHolder>().levelPlacement[0]);
            }
        }
    }
    public void spawnPlayer(GameObject player, int levelPlacement)
    {
        Debug.Log("player present");
        if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 1)
        {
            GameObject spawnCharacter = Instantiate(player, podium1st.transform.position, podium1st.transform.rotation) as GameObject;
        }
      else if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 2)
        {
            GameObject spawnCharacter = Instantiate(player, podium2st.transform.position, podium2st.transform.rotation) as GameObject;
        }
       else if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 3)
        {
            GameObject spawnCharacter = Instantiate(player, podium3st.transform.position, podium3st.transform.rotation) as GameObject;
        }
        else
        {
            GameObject spawnCharacter = Instantiate(player, podium1st.transform.position, podium1st.transform.rotation) as GameObject;
        }


    }

}
