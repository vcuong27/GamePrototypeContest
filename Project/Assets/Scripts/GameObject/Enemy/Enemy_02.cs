using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02 : BaseEnemy
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        float dt = Time.deltaTime;
        // TODO: Enemy move AI
        base.Update();

    }
}
