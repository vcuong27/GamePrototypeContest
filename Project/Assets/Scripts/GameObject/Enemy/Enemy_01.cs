using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : BaseEnemy
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        float dt = Time.deltaTime;
        // TODO: Enemy 1 move AI
        base.Update();
    }
}
