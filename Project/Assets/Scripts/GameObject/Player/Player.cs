using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameObject
{
    public static Player Instance;
    public List<BaseWeapons> m_listWP = new List<BaseWeapons>();


    //Damage take by enermy
    private DamageComponent m_CurrentDameTaken;
    //Weapons
    private BaseWeapons m_CurrentWeapons;

    private void Start()
    {
        m_CurrentWeapons = m_listWP[0];
        Instance = this;
        m_CurrentWeapons.SetUsing(true);
    }


    private void Update()
    {
        float dt = Time.deltaTime;

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


}
