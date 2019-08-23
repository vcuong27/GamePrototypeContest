using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //instance
    public static GameManager Instance;

    /// <summary>
    /// public variable
    /// </summary>
    public float m_ScreenWidth;
    public float m_ScreenHeight;
    public float M_SPAWN_COOLDOWN;
    public List<BaseEnemy> m_ListEnermyTemplate = new List<BaseEnemy>();


    public List<BaseEnemy> GetListEnermy()
    {
        return m_ListEnermy;
    }

    /// <summary>
    /// private variable
    /// </summary>
    private List<BaseEnemy> m_ListEnermy = new List<BaseEnemy>();
    private float m_DelaySpawn;


    private void Start()
    {
        Debug.Log("GameManager start");
        Instance = this;
        Screen.orientation = ScreenOrientation.Portrait;
        m_ScreenWidth = 20;
        m_ScreenHeight = 10;
        m_ListEnermy.AddRange(m_ListEnermyTemplate);
    }

    private void Update()
    {
        float dt = Time.deltaTime;


        // Spawn Enermy
        if (m_DelaySpawn <=  0)
        {
            m_DelaySpawn = M_SPAWN_COOLDOWN;
            BaseEnemy enermy = SpawnEnermy();
            enermy.transform.position = GetSpawnLocation();
        }
        else
        {
            m_DelaySpawn -= dt;
        }
    }


    private BaseEnemy SpawnEnermy()
    {
        foreach (var item in m_ListEnermy)
        {
            if (item.enabled == false)
            {
                return item;
            }
        }
        BaseEnemy enermy = Instantiate(m_ListEnermyTemplate[0], transform.position, Quaternion.identity);
        m_ListEnermy.Add(enermy);
        return enermy;
    }

    private Vector3 GetSpawnLocation()
    {
        Vector3 vec;
        // TODO: need to have random or special location to spawn enermy

        return vec;
    }

}
