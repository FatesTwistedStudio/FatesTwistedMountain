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
        if (other.GetComponent<S_CharInfoHolder>() == true)
        {
            eventController.GetComponent<S_EventController>().setTimedTrial(other.gameObject);
        }
        if (other.tag == "Player")
        {
            S_LeaderBoardTracker.firstPlacePointsText.SetText("" + other.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.firstPlaceTimeText.SetText("" + other.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
            other.gameObject.GetComponent<PlayerInput>().enabled = false;
            //            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            other.gameObject.GetComponent<S_HoverboardPhysic>().enabled = false;
            //play animation
            Invoke("pullUpLeaderBoard", 1);
            //List<S_CharacterCreate> characterCreate = new List<S_CharacterCreate>();
            //{
            //    characterCreate.Add(new S_CharacterCreate(other.name, other.GetComponent<S_CharInfoHolder>().timedTrial, other.GetComponent<S_CharInfoHolder>().pointsEarned));
            //}
        }
        if (other.tag == "Character")
        {
            //other.ai script== false
            //List<S_CharacterCreate> characterCreate = new List<S_CharacterCreate>();
            //{
            //    characterCreate.Add(new S_CharacterCreate(other.name, other.GetComponent<S_CharInfoHolder>().timedTrial, other.GetComponent<S_CharInfoHolder>().pointsEarned));
            //}
            if (S_LeaderBoardTracker.firstPlacePointsText == null)
            {
                S_LeaderBoardTracker.secondPlacePointsText.SetText("" + other.GetComponent<S_CharInfoHolder>().pointsEarned);
                S_LeaderBoardTracker.secondPlaceTimeText.SetText("" + other.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
            }
            if (S_LeaderBoardTracker.secondPlacePointsText == null)
            {
                S_LeaderBoardTracker.thirdPlacePointsText.SetText("" + other.GetComponent<S_CharInfoHolder>().pointsEarned);
                S_LeaderBoardTracker.thirdPlaceTimeText.SetText("" + other.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
            }
            if (S_LeaderBoardTracker.thirdPlacePointsText == null)
            {
                S_LeaderBoardTracker.fourthPlacePointsText.SetText("" + other.GetComponent<S_CharInfoHolder>().pointsEarned);
                S_LeaderBoardTracker.fourthPlaceTimeText.SetText("" + other.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
            }
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            // other.gameObject.GetComponent<S_HoverboardPhysic>().enabled = false;
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
