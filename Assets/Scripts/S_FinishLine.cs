using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class S_FinishLine : MonoBehaviour
{

    public S_LeaderBoardTracker LeaderBoardTracker;
    public GameObject eventController;
    public Canvas leaderboard;
    public GameObject firstPlace;
    public GameObject secondPlace;
    public GameObject thirdPlace;
    public GameObject fourthPlace;
    public GameObject NextLevelButton;
    public bool crossFinishLine;
    GameObject finishUI;
    public Animator anim;
    S_PauseMenu pm;

    public void winOrLoseTime(GameObject obj)
    {
        //WILL ONLY WORK IN SINGLE PLAYER MUST REVAMP FOR NPCS
        Debug.Log(obj.GetComponent<S_CharInfoHolder>().levelPlacement[0]);
        firstPlace = obj;
        secondPlace = null;
        thirdPlace = null;
        fourthPlace = null;
        obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 1;
        GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>().player.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 1;
    }

    //public void winOrLosePoint(GameObject obj)
    //{
    //    if (obj.GetComponent<S_CharInfoHolder>() != null)
    //    {
    //        firstPlace = null;
    //        secondPlace = null;
    //        thirdPlace = null;
    //        fourthPlace = obj;
    //        if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
    //            obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 4;
    //        if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
    //            obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 4;
    //        if (obj.GetComponent<S_CharInfoHolder>().pointsEarned < eventController.GetComponent<S_EventController>().bronzeLevelPoints[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
    //        {
    //            firstPlace = null;
    //            secondPlace = null;
    //            thirdPlace = obj;
    //            fourthPlace = null;
    //            if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
    //                obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 3;
    //            if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
    //                obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 3;
    //            if (obj.GetComponent<S_CharInfoHolder>().pointsEarned < eventController.GetComponent<S_EventController>().silverLevelPoints[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
    //            {
    //                firstPlace = null;
    //                secondPlace = obj;
    //                thirdPlace = null;
    //                fourthPlace = null;
    //                if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
    //                    obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 2;
    //                if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
    //                    obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 2;
    //                if (obj.GetComponent<S_CharInfoHolder>().pointsEarned < eventController.GetComponent<S_EventController>().goldLevelPoints[/*not gonna work*/SceneManager.GetActiveScene().buildIndex])
    //                {
    //                    firstPlace = obj;
    //                    secondPlace = null;
    //                    thirdPlace = null;
    //                    fourthPlace = null;
    //                    if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] == 0)
    //                        obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 1;
    //                    if (obj.GetComponent<S_CharInfoHolder>().levelPlacement[0] != 0)
    //                        obj.GetComponent<S_CharInfoHolder>().levelPlacement[1] = 1;
    //                }
    //            }
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        finishUI = GameObject.FindWithTag("FinishUI");
        if (other.GetComponent<S_CharInfoHolder>() == true)
            eventController.GetComponent<S_EventController>().setTimedTrial(other.gameObject);

        if (other.tag == "Character")
            //other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            other.GetComponent<S_HoverboardPhysic>().canMove = false;

        if (other.tag == "Player")
        {
            if (FindObjectOfType<S_LevelBGM>() != null)
            {

                pm.canPause = false;
                string levelMusic = FindObjectOfType<S_LevelBGM>().BackgroundSongName;
                FindObjectOfType<S_AudioManager>().FadeOut(levelMusic);
                Invoke("PlayMusic", 1);
            }

            crossFinishLine = true;

            //other.gameObject.GetComponent<PlayerInput>().enabled = false;
            other.GetComponent<S_HoverboardPhysic>().canMove = false;
            other.gameObject.GetComponent<S_PlayerInput>().Victory();
            other.gameObject.GetComponent<S_HoverboardPhysic>().maxSpeed = 0;

            winOrLoseTime(other.gameObject);

            finishUI.GetComponent<Canvas>().enabled = true;
            finishUI.GetComponent<Animator>().Play("a_UI_FinishLine");
            Invoke("pullUpLeaderBoard", 2);
        }

        if (other.tag == "GreenFlag")
        {
            other.GetComponent<S_CharInfoHolder>().itemHeld = null;
        }

        if (other.tag == "RedFlag")
        {
            other.GetComponent<S_CharInfoHolder>().itemHeld = null;
        }
    }

    public void PlayMusic()
    {
        FindObjectOfType<S_AudioManager>().FadeIn("Finish-Line");

    }

    public void pullUpLeaderBoard()
    {
        leaderboard.enabled = true;
        anim.Play("a_FinishLineStart");
    }

    void Start()
    {
        LeaderBoardTracker = FindObjectOfType<S_LeaderBoardTracker>();
        leaderboard = FindObjectOfType<S_LeaderBoardTracker>().gameObject.GetComponent<Canvas>();
        anim = FindObjectOfType<S_LeaderBoardTracker>().gameObject.GetComponent<Animator>();
        leaderboard.enabled = false;
        pm = FindObjectOfType<S_PauseMenu>();
    }

    public void sortTheWinners()
    {
        if (firstPlace != null)
        {
            LeaderBoardTracker.firstPlaceImage.sprite = firstPlace.GetComponent<S_CharInfoHolder>().image;
            LeaderBoardTracker.firstPlacePlacementText.SetText("" + firstPlace.GetComponent<S_CharInfoHolder>()._name);
            LeaderBoardTracker.firstPlacePointsText.SetText("" + firstPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            LeaderBoardTracker.firstPlaceTimeText.SetText("" + firstPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("Time:" + "0.000"));
        }
        else
        {
            LeaderBoardTracker.firstPlacePlacementText.SetText("Gold Spot");
            LeaderBoardTracker.firstPlacePointsText.SetText("--");
            LeaderBoardTracker.firstPlaceTimeText.SetText("--.--");
        }
        /*
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
        if (fourthPlace == null && thirdPlace == null && secondPlace == null && firstPlace == null)
        {
            Debug.Log("leader not set");
        }
        */
    }


    public void sendToNextLevel(string nextLevel)
    {
        Debug.Log("send credit music");
        SceneManager.LoadScene(nextLevel);
        FindObjectOfType<S_AudioManager>().FadeOut("Finish-Line");
    }

    private void Update()
    {
        //Note to self: Move this to Start for optimization
        eventController = GameObject.FindWithTag("EventController");
        if (fourthPlace != null || thirdPlace != null || secondPlace != null || firstPlace != null)
            sortTheWinners();
    }
}
