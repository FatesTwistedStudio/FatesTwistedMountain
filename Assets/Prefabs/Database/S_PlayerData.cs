using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit: https://www.youtube.com/watch?v=XOjd_qU2Ido&t=683s

[System.Serializable]
public class S_PlayerData
{
    public int[] highScores;

    public S_PlayerData(S_GameloopController playerData)
    {

        highScores = playerData.highscores;
    }

}
