using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterCreate : IComparable<S_CharacterCreate>
{
    // Start is called before the first frame update
    public string name;
    public float lapTime;
    public int points;
    public S_CharacterCreate(string newName, float recordedTime, int recordedPoints)
    {
        name = newName;
        lapTime = recordedTime;
        points = recordedPoints;
    }
    public int CompareTo(S_CharacterCreate other)
    {
        if (other == null)
        {
            return 1;
        }
        return points - other.points;
    }
    
}
