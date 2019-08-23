using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject : MonoBehaviour
{
    //Default value for game
    public float    M_MAX_HEAL     = 100;
    public float    M_MAX_ARMOR    = 100;

    //share varriable
    protected float m_Current_Heal;
    protected float m_Current_Armor;


    // Start is called before the first frame update
    public virtual void Start()
    {
        //init values
        m_Current_Heal = M_MAX_HEAL;
        m_Current_Armor = M_MAX_ARMOR;
        Debug.Log("m_Current_Heal " + m_Current_Heal);
    }

}
