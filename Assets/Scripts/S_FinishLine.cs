using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_FinishLine : MonoBehaviour
{
    public S_LeaderBoardTracker S_LeaderBoardTracker;
    public GameObject eventController;
    public Canvas leaderboard;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<S_CharInfoHolder>()==true)
        {
            eventController.GetComponent<S_EventController>().setTimedTrial(other.gameObject);
        }
        if (other.tag == "Player")
        {
            S_LeaderBoardTracker.firstPlacePointsText.SetText("" + other.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.firstPlaceTimeText.SetText("" + other.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
            other.gameObject.GetComponent<PlayerInput>().enabled = false;
            //play animation
            Invoke("pullUpLeaderBoard", 1);
            List<S_CharacterCreate> characterCreate = new List<S_CharacterCreate>();
            {
                characterCreate.Add(new S_CharacterCreate(other.name, other.GetComponent<S_CharInfoHolder>().timedTrial, other.GetComponent<S_CharInfoHolder>().pointsEarned));
            }
        }
        if (other.tag == "Character")
        {
            //other.ai script== false
            List<S_CharacterCreate> characterCreate = new List<S_CharacterCreate>();
            {
                characterCreate.Add(new S_CharacterCreate(other.name, other.GetComponent<S_CharInfoHolder>().timedTrial, other.GetComponent<S_CharInfoHolder>().pointsEarned));
            }
        }
    }
    public void pullUpLeaderBoard()
    {
        leaderboard.enabled = true;
    }
    void Start()
    {
        leaderboard.enabled = false;
    }
    public void sortTheWinners()
    {

    }
}
