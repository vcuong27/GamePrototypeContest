using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    // Receive/Give Damage
    public float HP => hp;
    public float MaxHP => maxHP;

    private float hp;
    private float maxHP;

    private float dot;
    private float dotInterval;
    private float nextDOTtick;
    private float dotTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (Time.time >= nextDOTtick)
        {
            if (Time.time < dotTimer)
            {
                hp -= dot;
            }
            while (nextDOTtick < Time.time)
            {
                nextDOTtick += dotInterval;
                hp -= dot;
            }
        }
        if (HP <= 0)
        {
            Destruct();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamagingComponent damaging = collision.gameObject.GetComponent<DamagingComponent>();
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
