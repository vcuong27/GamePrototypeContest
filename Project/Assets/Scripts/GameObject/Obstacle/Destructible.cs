using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : GameObject
{

    //Damage taken
    private List<DamageComponent> m_CurrentDameTaken;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
     
        if (HP <= 0)
        {
            Destruct();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageComponent damaging = collision.gameObject.GetComponent<DamageComponent>();
        if (damaging != null)
        {
            hp -= damaging.Damage;
            dot = damaging.DOT;
            dotInterval = damaging.DOTInterval;
            dotTimer = Time.time + damaging.DOTDuration + damaging.DOTInterval; // +damaging.DOTInterval for extratiming check
            nextDOTtick = Time.time + dotInterval;
        }
    }

    protected virtual void Destruct()
    {
        // TODO: Destruction goes here;
        // temporary
        Destroy(gameObject);
    }
}
