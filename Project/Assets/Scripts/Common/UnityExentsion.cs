using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExentsion
{

    public static float SignedEulerAngle(this float angle)
    {
        float signedAngle = angle % 360;
        if (signedAngle > 180)
        {
            signedAngle = 360 - signedAngle;
        }
        return signedAngle;
    }


    public static void LookAt2D(this Transform transform, Vector3 target)
    {
        target.z = 0;
        transform.up = target - transform.position;
    }


}
