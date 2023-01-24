using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class S_CanvasController : MonoBehaviour
{
    public GameObject levelSelect;
    public S_GameloopController S_GameloopController;
    public S_CharacterDatabase S_CharacterDatabase;
    public Image playerImage;
    public TextMeshProUGUI playerName;
    // Start is called before the first frame update
    private void Update()
    {
        if (S_GameloopController == null)
        {
            S_GameloopController = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>();
        }

        if (S_GameloopController != null)
        {
            if (S_GameloopController.player != null)
            {
                levelSelect.SetActive(true);
                setPlayerFacingUI();
            }
            else
            {
                levelSelect.SetActive(false);
            }
        }
    }
    public void setPlayerFacingUI()
    {
        playerImage.sprite = S_GameloopController.player.GetComponent<S_CharInfoHolder>().image;
    playerName.text= S_GameloopController.player.GetComponent<S_CharInfoHolder>()._name;
    }
    public void SetCharacter(int rosterNum)
    {
        S_GameloopController.player = S_CharacterDatabase.GetComponent<S_CharacterDatabase>().characterInformation[rosterNum].characterPrefab;
        S_GameloopController.player.GetComponent<S_CharInfoHolder>().image = S_CharacterDatabase.GetComponent<S_CharacterDatabase>().characterInformation[rosterNum].characterImage;
        S_GameloopController.player.GetComponent<S_CharInfoHolder>()._name = S_CharacterDatabase.GetComponent<S_CharacterDatabase>().characterInformation[rosterNum].characterName;
    }
}
