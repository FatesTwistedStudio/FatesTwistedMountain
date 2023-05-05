using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class S_Credits : MonoBehaviour
{
    public S_CreditsDatabase creditsDatabase;
    public float speed = 100.0f;
    public float textPosition = -1000.0f;
    public float boundaryTextEnd = 3745f;

    public RectTransform boundaryRectTransform;

    public TextMeshProUGUI mainText;

    public bool isLooping = false;
    private void Start()
    {
        boundaryRectTransform = GetComponent<RectTransform>();
        StartCoroutine("AutoScrollText");
    }
    IEnumerable AutoScrollText()
    {
        while (boundaryRectTransform.localPosition.y < boundaryTextEnd)
        {
            boundaryRectTransform.Translate(Vector3.up * speed * Time.deltaTime);
            yield return null;
        }
    }

    public void sendToNextLevel(string nextLevel)
    {
        SceneManager.LoadScene(nextLevel);
        FindObjectOfType<S_AudioManager>().StopPlaying("Finish-Line");
    }
}
