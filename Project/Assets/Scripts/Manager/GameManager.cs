using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public float m_ScreenWidth;
    public float m_ScreenHeight;


    void Start()
    {
        Debug.Log("GameManager start");
        Instance = this;
        Screen.orientation = ScreenOrientation.Portrait;
        m_ScreenWidth = 20;
        m_ScreenHeight = 10;
    }

    void Update()
    {
        
    }
}
