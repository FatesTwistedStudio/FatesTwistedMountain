using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_LeaderBoardTracker : MonoBehaviour
{
    public TextMeshProUGUI firstPlacePlacementText;
    public TextMeshProUGUI firstPlaceTimeText;
    public TextMeshProUGUI firstPlacePointsText;
    public TextMeshProUGUI secondPlacePlacementText;
    public TextMeshProUGUI secondPlaceTimeText;
    public TextMeshProUGUI secondPlacePointsText;
    public TextMeshProUGUI thirdPlacePlacementText;
    public TextMeshProUGUI thirdPlaceTimeText;
    public TextMeshProUGUI thirdPlacePointsText;
    public TextMeshProUGUI fourthPlacePlacementText;
    public TextMeshProUGUI fourthPlaceTimeText;
    public TextMeshProUGUI fourthPlacePointsText;

    public Image firstPlaceImage;
    public Image secondPlaceImage;
    public Image thirdPlaceImage;
    public Image fourthPlaceImage;

    private void Start()
    {
        firstPlacePlacementText.SetText("");
        secondPlacePlacementText.SetText("");
        thirdPlacePlacementText.SetText("");
        fourthPlacePlacementText.SetText("");
        firstPlacePointsText.SetText("");
        firstPlaceTimeText.SetText("");
        secondPlacePointsText.SetText("");
        secondPlaceTimeText.SetText("");
        thirdPlacePointsText.SetText("");
        thirdPlaceTimeText.SetText("");
        fourthPlacePointsText.SetText("");
        fourthPlaceTimeText.SetText("");
    } //create a list for storing character data 

    private void Update()
    {
       
    }

}
