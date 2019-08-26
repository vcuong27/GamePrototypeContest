using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public delegate IEnumerator OnDestructCallBacks(Destructible destructible);
    // Receive/Give Damage
    public float HP => hp;
    public float MaxHP => maxHP;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float dot;
    [SerializeField]
    private float dotInterval;
    [SerializeField]
    private float nextDOTtick;
    [SerializeField]
    private float dotTimer;
    public event OnDestructCallBacks OnDestruct;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float dt = Time.deltaTime;

        if (HP <= 0)
        {
            StartCoroutine(Destruct());
        }

        if (Time.time >= nextDOTtick && dotInterval > 0)
        {
            if (Time.time < dotTimer)
            {
                hp -= dot;
            }
            while (nextDOTtick < Time.time && nextDOTtick < dotTimer)
            {
                nextDOTtick += dotInterval;
                hp -= dot;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hit");
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

    private void OnEnable()
    {
        hp = maxHP;
    }
    protected virtual IEnumerator Destruct()
    {
        // TODO: Destruction goes here;
        // temporary
        Debug.Log("ded");
        yield return OnDestruct?.Invoke(this);
        //yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
