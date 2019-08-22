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
       // m_listWP.Add(new Pistol());
        m_CurrentWeapons = m_listWP[0];
        Instantiate(m_CurrentWeapons, transform.position, Quaternion.identity);
        Instance = this;
    }


    private void Update()
    {
        float dt = Time.deltaTime;

    }


}
