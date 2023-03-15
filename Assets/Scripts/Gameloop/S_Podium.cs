using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Podium : MonoBehaviour
{
    public GameObject player;
    public GameObject podium1st;
    public GameObject podium2st;
    public GameObject podium3st;
    private void Update()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 1)
                Instantiate(player, podium1st.transform.position, podium1st.transform.rotation);
            if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 2)
                Instantiate(player, podium2st.transform.position, podium2st.transform.rotation);
            if (player.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 3)
                Instantiate(player, podium3st.transform.position, podium3st.transform.rotation);
        }
    }

}
