using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : BaseWeapons
{

    private void Start()
    {
        // set damage for Pistol
        m_DameComponent = new DamageComponent(10,0,0,0);
        base.Start();
    }

    private void Update()
    {
        base.Update();
    }

}
