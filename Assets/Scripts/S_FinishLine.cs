using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
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
 
    public void winOrLose(GameObject obj)
    {
        if (obj.GetComponent<S_CharInfoHolder>() != null)
        {
            firstPlace = null; 
            secondPlace = null;
            thirdPlace = null;
            fourthPlace = obj;
            if (obj.GetComponent<S_CharInfoHolder>().timedTrial < eventController.GetComponent<S_EventController>().bronzeLevelTimes[0/*based on scene? */])
            {
                firstPlace = null;
                secondPlace = null;
                thirdPlace = obj;
                fourthPlace = null;
                if (obj.GetComponent<S_CharInfoHolder>().timedTrial < eventController.GetComponent<S_EventController>().silverLevelTimes[0/*based on scene? */])
                {
                    firstPlace = null;
                    secondPlace = obj;
                    thirdPlace = null;
                    fourthPlace = null;
                    if (obj.GetComponent<S_CharInfoHolder>().timedTrial < eventController.GetComponent<S_EventController>().goldLevelTimes[0/*based on scene? */])
                    {
                        firstPlace = obj;
                        secondPlace = null;
                        thirdPlace = null;
                        fourthPlace = null;
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
            winOrLose(other.gameObject);
            //if (firstPlace == null)
            //{
            //    firstPlace = other.gameObject;

            //    return;
            //}
            //if (firstPlace != null)
            //{
            //    if (secondPlace == null)
            //    {
            //        if (other.gameObject != firstPlace)
            //        {
            //            secondPlace = other.gameObject;
            //            return;
            //        }
            //    }
            //    if (secondPlace != null)
            //    {
            //        if (thirdPlace == null)
            //        {
            //            if (other.gameObject != firstPlace)
            //            {
            //                if (other.gameObject != secondPlace)
            //                {
            //                    thirdPlace = other.gameObject;
            //                    return;
            //                }
            //            }
            //        }
            //        if (thirdPlace != null)
            //        {
            //            if (fourthPlace == null)
            //            {
            //                if (other.gameObject != firstPlace)
            //                {
            //                    if (other.gameObject != secondPlace)
            //                    {
            //                        if (other.gameObject != thirdPlace)
            //                        {
            //                            fourthPlace = other.gameObject;
            //                            return;
            //                        }
            //                    }
            //                }
            //            }
            //            if (fourthPlace != null)
            //            {
            //                if (other.gameObject != firstPlace)
            //                {
            //                    if (other.gameObject != secondPlace)
            //                    {
            //                        if (other.gameObject != thirdPlace)
            //                        {
            //                            if (other.gameObject != fourthPlace)
            //                            {
            //                                Debug.Log("Game Over");
            //                                return;
            //                            }
            //                        }
            //                    }
            //                }

            //            }
            //        }
            //    }
            //}
        }
        if (other.tag == "Character")
        {
            //other.ai script== false
            //List<S_CharacterCreate> characterCreate = new List<S_CharacterCreate>();
            //{
            //    characterCreate.Add(new S_CharacterCreate(other.name, other.GetComponent<S_CharInfoHolder>().timedTrial, other.GetComponent<S_CharInfoHolder>().pointsEarned));
            //}
            if (firstPlace == null)
            {
                firstPlace = other.gameObject;

                return;
            }
            if (firstPlace != null)
            {
                if (secondPlace == null)
                {
                    if (other.gameObject != firstPlace)
                    {
                        secondPlace = other.gameObject;
                        return;
                    }
                }
                if (secondPlace != null)
                {
                    if (thirdPlace == null)
                    {
                        if (other.gameObject != firstPlace)
                        {
                            if (other.gameObject != secondPlace)
                            {
                                thirdPlace = other.gameObject;
                                return;
                            }
                        }
                    }
                    if (thirdPlace != null)
                    {
                        if (fourthPlace == null)
                        {
                            if (other.gameObject != firstPlace)
                            {
                                if (other.gameObject != secondPlace)
                                {
                                    if (other.gameObject != thirdPlace)
                                    {
                                        fourthPlace = other.gameObject;
                                        return;
                                    }
                                }
                            }
                        }
                        if (fourthPlace != null)
                        {
                            if (other.gameObject != firstPlace)
                            {
                                if (other.gameObject != secondPlace)
                                {
                                    if (other.gameObject != thirdPlace)
                                    {
                                        if (other.gameObject != fourthPlace)
                                        {
                                            Debug.Log("Game Over");
                                            return;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
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
        if (firstPlace != null)
        {
            S_LeaderBoardTracker.firstPlaceImage.sprite = firstPlace.GetComponent<S_CharInfoHolder>().image;
            S_LeaderBoardTracker.firstPlacePlacementText.SetText("" + firstPlace.GetComponent<S_CharInfoHolder>()._name);
            S_LeaderBoardTracker.firstPlacePointsText.SetText("" + firstPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.firstPlaceTimeText.SetText("" + firstPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
        }
        if (secondPlace != null)
        {
            S_LeaderBoardTracker.secondPlaceImage.sprite = secondPlace.GetComponent<S_CharInfoHolder>().image;
            S_LeaderBoardTracker.secondPlacePlacementText.SetText("" + secondPlace.GetComponent<S_CharInfoHolder>()._name);
            S_LeaderBoardTracker.secondPlacePointsText.SetText("" + secondPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.secondPlaceTimeText.SetText("" + secondPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
        }
        if (thirdPlace != null)
        {
            S_LeaderBoardTracker.thirdPlaceImage.sprite = thirdPlace.GetComponent<S_CharInfoHolder>().image;
            S_LeaderBoardTracker.thirdPlacePlacementText.SetText("" + thirdPlace.GetComponent<S_CharInfoHolder>()._name);
            S_LeaderBoardTracker.thirdPlacePointsText.SetText("" + thirdPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.thirdPlaceTimeText.SetText("" + thirdPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
        }
        if (fourthPlace != null)
        {
            S_LeaderBoardTracker.fourthPlaceImage.sprite = fourthPlace.GetComponent<S_CharInfoHolder>().image;
            S_LeaderBoardTracker.fourthPlacePlacementText.SetText("" + fourthPlace.GetComponent<S_CharInfoHolder>()._name);
            S_LeaderBoardTracker.fourthPlacePointsText.SetText("" + fourthPlace.GetComponent<S_CharInfoHolder>().pointsEarned);
            S_LeaderBoardTracker.fourthPlaceTimeText.SetText("" + fourthPlace.GetComponent<S_CharInfoHolder>().timedTrial.ToString("0.00"));
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
