using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExentsion
{

    //public static void LookAt2D(this Transform transform, Vector3 target)
    //{
    //    Vector3 newUp = target - transform.position;
    //    newUp.z = 0;
    //    transform.up = newUp;
    //}


    public static Color GetColor(this string fromHex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(fromHex, out color))
            return color;
        return new Color();

    }
}
