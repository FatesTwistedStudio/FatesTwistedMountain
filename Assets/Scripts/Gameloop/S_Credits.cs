using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class S_Credits : MonoBehaviour
{
    public S_CreditsDatabase creditsDatabase;

   
    [SerializeField]
    public GameObject[] contributorLogo;
    public TextMeshProUGUI[] contributorName;
    public TextMeshProUGUI[] contributorRole;
    public TextMeshProUGUI[] contributorThanks;

    private void Update()
    {
        for (int i = 0; i < creditsDatabase.creditsInformation.Length; i++)
        {
            if (creditsDatabase.creditsInformation[i] != null)
            {
                if (creditsDatabase.creditsInformation[i].stickerSprite != null)
                    contributorLogo[i].GetComponent<Image>().sprite = creditsDatabase.creditsInformation[i].stickerSprite;
                if (creditsDatabase.creditsInformation[i].contributorsName != null)
                    contributorName[i].SetText("" + creditsDatabase.creditsInformation[i].contributorsName[i]);
                if (creditsDatabase.creditsInformation[i].contributorsRole != null)
                    contributorRole[i].SetText("" + creditsDatabase.creditsInformation[i].contributorsRole[i]);
                if (creditsDatabase.creditsInformation[i].contributorsThanks != null)
                    contributorThanks[i].SetText("" + creditsDatabase.creditsInformation[i].contributorsThanks[i]);
            }
        }
    }
}
