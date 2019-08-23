using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02 : BaseEnemy
{
    public override void Start()
    {
        m_DameComponent = new DamageComponent(M_DAMAGE_01, M_DAMAGE_02, M_DAMAGE_INTERVAL, M_DAMAGE_DURATION);
        base.Start();
    }

    public override void Update()
    {
        base.Update();

    }
}
