using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CreditsDatabase : MonoBehaviour
{
    [System.Serializable]
    public class CreditsInfo
    {
        public string contributorsName;
        public string contributorsRole;
        public string contributorsThanks;
        public Sprite stickerSprite;

    }
    public CreditsInfo[] creditsInformation = new CreditsInfo[1];
}
