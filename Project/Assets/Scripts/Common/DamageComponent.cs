using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Still contact with Destroyable but cannot receive damage
public class DamageComponent 
{
    public float Damage => m_Damage; // impact damage
    public float DOT => m_Dot;// damage over time
    public float DOTDuration => m_DotDuration;
    public float DOTInterval => m_DotInterval;

    private float m_Damage;
    private float m_Dot;
    private float m_DotInterval;
    private float m_DotDuration;

    public DamageComponent(float damage, float dot, float dotInterval, float dotDuration)
    {
        m_Damage = damage;
        m_Dot = dot;
        m_DotInterval = dotInterval;
        m_DotDuration = dotDuration;
    }
   
}
