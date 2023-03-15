using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class S_FinishLine : MonoBehaviour
{

    public S_LeaderBoardTracker S_LeaderBoardTracker;
    public GameObject eventController;
    public Canvas leaderboard;
    public GameObject firstPlace;
    public GameObject secondPlace;
    public GameObject thirdPlace;
    public GameObject fourthPlace;

    public void winOrLoseTime(GameObject obj)
    {
        if (obj.GetComponent<S_CharInfoHolder>() != null)
        {

            firstPlace = null;
            secondPlace = null;
            thirdPlace = null;
            fourthPlace = obj;
            if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
                obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 4;
            if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
                obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 4;

            if (obj.GetComponent<S_CharInfoHolder>().timedTrial < eventController.GetComponent<S_EventController>().bronzeLevelTimes[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
            {
                firstPlace = null;
                secondPlace = null;
                thirdPlace = obj;
                fourthPlace = null;
                if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
                    obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 3;
                if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
                    obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 3;


                if (obj.GetComponent<S_CharInfoHolder>().timedTrial < eventController.GetComponent<S_EventController>().silverLevelTimes[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
                {
                    firstPlace = null;
                    secondPlace = obj;
                    thirdPlace = null;
                    fourthPlace = null;
                    if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
                        obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 2;
                    if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
                        obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 2;
                    if (obj.GetComponent<S_CharInfoHolder>().timedTrial < eventController.GetComponent<S_EventController>().goldLevelTimes[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
                    {
                        firstPlace = obj;
                        secondPlace = null;
                        thirdPlace = null;
                        fourthPlace = null;
                        if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
                            obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 1;
                        if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
                            obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 1;
                    }
                }
            }
        }
    }
    public void winOrLosePoint(GameObject obj)
    {
        if (obj.GetComponent<S_CharInfoHolder>() != null)
        {
            firstPlace = null;
            secondPlace = null;
            thirdPlace = null;
            fourthPlace = obj;
            if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
                obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 4;
            if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
                obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 4;
            if (obj.GetComponent<S_CharInfoHolder>().pointsEarned < eventController.GetComponent<S_EventController>().bronzeLevelPoints[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
            {
                firstPlace = null;
                secondPlace = null;
                thirdPlace = obj;
                fourthPlace = null;
                if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
                    obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 3;
                if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
                    obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 3;
                if (obj.GetComponent<S_CharInfoHolder>().pointsEarned < eventController.GetComponent<S_EventController>().silverLevelPoints[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
                {
                    firstPlace = null;
                    secondPlace = obj;
                    thirdPlace = null;
                    fourthPlace = null;
                    if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
                        obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 2;
                    if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
                        obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 2;
                    if (obj.GetComponent<S_CharInfoHolder>().pointsEarned < eventController.GetComponent<S_EventController>().goldLevelPoints[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
                    {
                        firstPlace = obj;
                        secondPlace = null;
                        thirdPlace = null;
                        fourthPlace = null;
                        if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
                            obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 1;
                        if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
                            obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 1;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<S_CharInfoHolder>() == true)
        {
            eventController.GetComponent<S_EventController>().setTimedTrial(other.gameObject);
        }
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerInput>().enabled = false;
            //play animation
            Invoke("pullUpLeaderBoard", 1);
            if (eventController.GetComponent<S_EventController>().isTimedEvent)
            {
                winOrLoseTime(other.gameObject);
                eventController.GetComponent<S_EventController>().endTimedRace();

            }
            if (eventController.GetComponent<S_EventController>().isTimedEvent == false)
            {
                winOrLosePoint(other.gameObject);
            }

        }
        if (other.tag == "Character")
        {
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
        if (firstPlace != null)
        {
            S_LeaderBoardTracker.firstPlaceImage.sprite = firstPlace.GetComponent<S_CharInfoHolder>().image;
            S_LeaderBoardTracker.firstPlacePlacementText.SetText("Gold");
            S_LeaderBoardTracker.firstPlacePointsText.SetText("" + firstPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.firstPlaceTimeText.SetText("" + firstPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
        }
        else
        {

            S_LeaderBoardTracker.firstPlacePlacementText.SetText("Gold Spot");
            S_LeaderBoardTracker.firstPlacePointsText.SetText("--");
            S_LeaderBoardTracker.firstPlaceTimeText.SetText("--.--");
        }
        if (secondPlace != null)
        {
            S_LeaderBoardTracker.secondPlaceImage.sprite = secondPlace.GetComponent<S_CharInfoHolder>().image;
            S_LeaderBoardTracker.secondPlacePlacementText.SetText("Silver");
            S_LeaderBoardTracker.secondPlacePointsText.SetText("" + secondPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.secondPlaceTimeText.SetText("" + secondPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
        }
        else
        {
            S_LeaderBoardTracker.secondPlacePlacementText.SetText("Silver Spot");
            S_LeaderBoardTracker.secondPlacePointsText.SetText("--");
            S_LeaderBoardTracker.secondPlaceTimeText.SetText("--.--");
        }
        if (thirdPlace != null)
        {
            S_LeaderBoardTracker.thirdPlaceImage.sprite = thirdPlace.GetComponent<S_CharInfoHolder>().image;
            S_LeaderBoardTracker.thirdPlacePlacementText.SetText("Bronze");
            S_LeaderBoardTracker.thirdPlacePointsText.SetText("" + thirdPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.thirdPlaceTimeText.SetText("" + thirdPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
        }
        else
        {
            S_LeaderBoardTracker.thirdPlacePlacementText.SetText("Bronze Spot");
            S_LeaderBoardTracker.thirdPlacePointsText.SetText("--");
            S_LeaderBoardTracker.thirdPlaceTimeText.SetText("--.--");
        }
        if (fourthPlace != null)
        {
            S_LeaderBoardTracker.fourthPlaceImage.sprite = fourthPlace.GetComponent<S_CharInfoHolder>().image;
            S_LeaderBoardTracker.fourthPlacePlacementText.SetText("Participation");
            S_LeaderBoardTracker.fourthPlacePointsText.SetText("" + fourthPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.fourthPlaceTimeText.SetText("" + fourthPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
        }
        else
        {
            S_LeaderBoardTracker.fourthPlacePlacementText.SetText("Loser Spot");
            S_LeaderBoardTracker.fourthPlacePointsText.SetText("--");
            S_LeaderBoardTracker.fourthPlaceTimeText.SetText("--.--");
        }
    }
    public void sendToNextLevel(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
    }
    private void Update()
    {
        eventController = GameObject.FindWithTag("EventController");
        sortTheWinners();
    }
}
