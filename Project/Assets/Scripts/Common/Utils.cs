using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static float DistanceBetweenTwoPoint(Vector2 pos1, Vector2 pos2)
    {
        return (pos1 - pos2).sqrMagnitude;
    }
}
