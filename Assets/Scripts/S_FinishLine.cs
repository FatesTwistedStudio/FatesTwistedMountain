using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_FinishLine : MonoBehaviour
{
    public S_LeaderBoardTracker S_LeaderBoardTracker;
    public Canvas leaderboard;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<S_CharInfoHolder>() != null)
        {

            S_LeaderBoardTracker.sortWinners(/*send character info*/) ;
        }
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerInput>().enabled=false;
            //play animation
            Invoke("pullUpLeaderBoard", 1);
        }
        if(other.tag=="Character")
        {
            //other.ai script== false
        }
    }
    public void pullUpLeaderBoard()
    {
        leaderboard.enabled=true;
       
    }
    void Start()
    {
        leaderboard.enabled=false;
    }
}
