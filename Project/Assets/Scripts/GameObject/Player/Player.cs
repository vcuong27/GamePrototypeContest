using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameObject
{
    public static Player Instance;
    public List<BaseWeapons> m_listWP = new List<BaseWeapons>();


    //Damage take by enermy
    private List<DamageComponent> m_CurrentDameTaken;
    //Weapons
    private BaseWeapons m_CurrentWeapons;
    private bool m_IsDie;

    private void Start()
    {
        m_CurrentWeapons = m_listWP[0];
        Instance = this;
        m_CurrentWeapons.SetUsing(true);
        m_IsDie = false;
        base.Start();
    }


    private void Update()
    {
        float dt = Time.deltaTime;

        //check die condition
        if (m_IsDie)
        {
            Die();
            return;
        }

        //choose target and attack
        BaseEnemy enemy = FindNearestEnermy();
        if (Utils.DistanceBetweenTwoPoint(transform.position, enemy.transform.position) < m_CurrentWeapons.GetAttackRange())
        {
            Attack(enemy);
        }
        else
        {
            m_CurrentWeapons.SetUsing(false);
        }


        //Handle damage
        UpdateDamage(dt);


        //change weapon
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            m_CurrentWeapons.SetUsing(false);
            m_CurrentWeapons = m_listWP[0];
            m_CurrentWeapons.SetUsing(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            m_CurrentWeapons.SetUsing(false);
            m_CurrentWeapons = m_listWP[1];
            m_CurrentWeapons.SetUsing(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            m_CurrentWeapons.SetUsing(false);
            m_CurrentWeapons = m_listWP[2];
            m_CurrentWeapons.SetUsing(true);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: need implement collision 
        //check collision with something and take damage (from bullet or enermy)
        //DamageComponent dmg = enemy bullet
        //TakeDamage(dmg);
    }

    private BaseEnemy FindNearestEnermy()
    {
        BaseEnemy enemy = GameManager.Instance.GetListEnermy()[0];

        float d = Utils.DistanceBetweenTwoPoint(enemy.transform.position, transform.position);

        Debug.Log("FindNearestEnermy " + d);
        foreach (var item in GameManager.Instance.GetListEnermy())
        {
            float d1 = Utils.DistanceBetweenTwoPoint(item.transform.position, transform.position);
            if (d > d1)
            {
                enemy = item;
                d = d1;
            }
        }

        return enemy;
    }

    private void Attack(BaseEnemy enemy)
    {
        m_CurrentWeapons.SetFireTarget(enemy.transform.position);
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
                if (item.m_Damage > 0)
                {
                    m_Current_Heal -= item.m_Damage;
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
        // TODO: need implement die vfx and show defeat screen
        m_IsDie = false;
        enabled = false;
        GetComponent<Renderer>().enabled = false;
    }

}
