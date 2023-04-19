using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Diagnostics.CodeAnalysis;

public class S_CanvasController : MonoBehaviour
{
    public bool needsToToggle = false;
    public GameObject levelSelect;
    public GameObject characterSelect;
    public GameObject testlevel;
    public GameObject characterContinueButton;
    public S_GameloopController S_GameloopController;
    public S_CharacterDatabase S_CharacterDatabase;
    public Image playerImage;
    public TextMeshProUGUI playerName;

    public GameObject SelectLevelButton, SelectCharacterButton;

     

    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            testlevel.SetActive(true);
      
        if (Input.GetKeyUp(KeyCode.Q))
            testlevel.SetActive(false);

        if (S_GameloopController == null)
            S_GameloopController = GameObject.FindWithTag("GameController").GetComponent<S_GameloopController>();

        if (S_GameloopController != null)
            if (S_GameloopController.player != null)
            {
                setPlayerFacingUI();
                characterContinueButton.SetActive(true);
            }
        if (S_GameloopController.player == null)
        {
            characterContinueButton.SetActive(false);
            levelSelect.SetActive(false);
        }

        if (needsToToggle == true)
        {
            turnOnLevelSelect();
            needsToToggle = false;
        }

    }
    public void setPlayerFacingUI()
    {
        //playerImage.sprite = S_GameloopController.player.GetComponent<S_CharInfoHolder>().image;
        playerName.text = S_GameloopController.player.GetComponent<S_CharInfoHolder>()._name;
    }
    public void SetCharacter(int rosterNum)
    {
        S_GameloopController.player = S_CharacterDatabase.GetComponent<S_CharacterDatabase>().characterInformation[rosterNum].characterPrefab;
        S_GameloopController.player.GetComponent<S_CharInfoHolder>().image = S_CharacterDatabase.GetComponent<S_CharacterDatabase>().characterInformation[rosterNum].characterImage;
        S_GameloopController.player.GetComponent<S_CharInfoHolder>()._name = S_CharacterDatabase.GetComponent<S_CharacterDatabase>().characterInformation[rosterNum].characterName;
        S_GameloopController.player.GetComponent<S_CharInfoHolder>().levelPlacement[0] = 0;
    }
    public void turnOnLevelSelect()
    {
        levelSelect.gameObject.SetActive(true);
        characterSelect.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SelectLevelButton);

    }
    public void turnOffLevelSelect()
    {
        levelSelect.gameObject.SetActive(false);
        characterSelect.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SelectCharacterButton);

    }
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void turnOnButton(Button level)
    {
        level.gameObject.SetActive(true);
    }
}
