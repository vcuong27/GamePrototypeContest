using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : CustumGameObject
{
    public BaseWeapons m_CurrentWeapons;

    //Damage taken
    protected List<DamageComponent> m_CurrentDameTaken = new List<DamageComponent>();
    protected bool m_IsDie;

    public virtual void Start()
    {
        m_CurrentWeapons.SetUsing(false);
        m_IsDie = false;
        base.Start();
    }

    public virtual void Update()
    {
        float dt = Time.deltaTime;

        //check die condition
        if (m_IsDie)
        {
            Die();
            return;
        }

        //attack
        if (Utils.DistanceBetweenTwoPoint(transform.position, Player.Instance.transform.position) < m_CurrentWeapons.GetAttackRange())
        {
            Attack();
        }
        else
        {
            m_CurrentWeapons.SetUsing(false);
        }

        //Handle damage
        UpdateDamage(dt);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: need implement collision 
        //check collision with something and take damage (from bullet or player)
        //DamageComponent dmg = player bullet
        //TakeDamage(dmg);
    }

    public void SetActive(bool active)
    {
        enabled = active;
        GetComponent<Renderer>().enabled = active;
    }

    private void Attack()
    {
        m_CurrentWeapons.SetFireTarget(Player.Instance.transform.position);
    }

    private void TakeDamage(DamageComponent dmg)
    {
        m_CurrentDameTaken.Add(dmg);
    }

    private void UpdateDamage(float dt)
    {
        foreach (var item in m_CurrentDameTaken)
        {
            // physic damage
            if (item.m_Damage > 0)
            {
                if (m_Current_Armor > 0)
                {
                    m_Current_Armor -= item.m_Damage;
                    if (m_Current_Armor > 0)
                    {
                        item.m_Damage = 0;
                    }
                    else
                    {
                        item.m_Damage = 0 - m_Current_Armor;
                        m_Current_Armor = 0;
                    }
                }
                if (item.Damage > 0)
                {
                    m_Current_Heal -= item.Damage;
                    item.m_Damage = 0;
                }

            }

            if (item.DOT > 0 && item.m_DotDuration > 0)
            {
                item.m_DotDuration -= dt;
                if (item.m_DotDuration <= 0)
                    item.m_Dot = 0;

                // TODO: need check this dame type
                //m_Current_Heal -= item.Damage;
            }

            if (m_Current_Heal <= 0)
            {
                m_IsDie = true;
                break;
            }
            if (item.m_Damage <= 0 && item.m_Dot <= 0)
                m_CurrentDameTaken.Remove(item);
        }
    }

    private void Die()
    {
        // TODO: need implement die vfx 
        m_IsDie = false;
        SetActive(false);
    }


}
