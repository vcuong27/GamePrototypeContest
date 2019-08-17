using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExentsion
{

    public static void LookAt2D(this Transform transform, Vector3 target)
    {
        target.z = 0;
        transform.up = target - transform.position;
    }


}
