using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_VersionNumber : MonoBehaviour
{
    private TMP_Text versionText;

    // Start is called before the first frame update
    void Start()
    {
        // Find the TextMeshPro component on the same GameObject
        versionText = GetComponent<TMP_Text>();

        // Check if the TextMeshPro component is found
        if (versionText == null)
        {
            Debug.LogError("TextMeshPro component not found on the GameObject!");
            return;
        }

        // Update the text to display the version number
        versionText.text = "Version: " + Application.version;
    }
}
