using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : GameObject
{
    public BaseWeapons m_CurrentWeapons;

    //Damage taken
    protected List<DamageComponent> m_CurrentDameTaken;

    public virtual void Start()
    {
        m_ListBullet = new List<Bullet>();
        m_CurrentWeapons.SetUsing(false);
        base.Start();
    }

    public virtual void Update()
    {
        if (Utils.DistanceBetweenTwoPoint(transform.position, Player.Instance.transform.position) < m_CurrentWeapons.GetAttackRange())
        {
            Attack();
        }
        else
        {
            m_CurrentWeapons.SetUsing(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check collision with something
    }


    private void Attack()
    {
        m_CurrentWeapons.SetFireTarget(Player.Instance.transform.position);
    }
}
