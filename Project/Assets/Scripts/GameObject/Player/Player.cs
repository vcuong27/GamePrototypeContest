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

    private void Start()
    {
        m_CurrentWeapons = m_listWP[0];
        Instance = this;
        m_CurrentWeapons.SetUsing(true);
        base.Start();
    }


    private void Update()
    {
        float dt = Time.deltaTime;


        //choose target
        BaseEnemy enemy = FindNearestEnermy();
        if (Utils.DistanceBetweenTwoPoint(transform.position, enemy.transform.position) < m_CurrentWeapons.GetAttackRange())
        {
            Attack();
        }
        else
        {
            m_CurrentWeapons.SetUsing(false);
        }


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
        // check collision with something
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

    private void Attack()
    {
        m_CurrentWeapons.SetFireTarget(enemy.transform.position);
    }

}
