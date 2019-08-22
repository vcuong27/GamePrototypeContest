using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapons : GameObject
{
    public Bullet M_BULLET;
    public int M_NUMBER_BULLETS = 1;
    public float M_RELOAD = 10.0f;
    public float M_FIRE_RATE = 0.05f;
    public float M_BULLET_SPEED = 10.0f;

    protected DamageComponent m_DameComponent;

    private Vector2 m_Target;
    private float m_delayFire;
    private float m_delayReload;
    private float m_CurrentBullet;
    private bool m_IsFire;
    private List<Bullet> m_ListBullet;

    public virtual void Start()
    {
        m_CurrentBullet = M_NUMBER_BULLETS;
        m_delayFire = M_FIRE_RATE;
        m_delayReload = M_RELOAD;
        m_delayFire = 0.0f;
        m_IsFire = true;
        Debug.Log("BaseWeapons Start");
        m_ListBullet = new List<Bullet>();

    }

    public virtual void Update()
    {
        float dt = Time.deltaTime;
        //fire and reload 
        if (m_IsFire)
        {
            //fire
            if (m_delayFire < 0)
            {
                Fire();
                m_delayFire = M_FIRE_RATE;
            }
            else
            {
                m_delayFire -= dt;
            }
        }
        else
        {
            //reload
            if (m_delayReload <= 0)
            {
                Debug.Log("reloaded");
                m_IsFire = true;
                m_delayReload = M_RELOAD;
                m_CurrentBullet = M_NUMBER_BULLETS;
            }
            else
            {
                Debug.Log("reload--------");
                m_delayReload -= dt;
            }
        }
    }

    public void setFireTarget(Vector2 target)
    {
        m_Target = target;
    }

    private void Fire()
    {
        Bullet bullet = SpawnBullet();
        bullet.enabled = true;


        //caculate 
        bullet.SetVelocity(GetVeclocity());
        bullet.SetDamageComponent(m_DameComponent);
        bullet.SetPosition(Player.Instance.transform.position);

        m_CurrentBullet--;
        if (m_CurrentBullet <= 0)
        {
            m_IsFire = false;
        }
    }

    private Bullet SpawnBullet()
    {
        foreach (var item in m_ListBullet)
        {
            if (item.enabled == false)
            {
                Debug.Log("SpawnBullet 1");
                return item;
            }
        }
        Debug.Log("SpawnBullet 2");

        Bullet bullet = Instantiate(M_BULLET, transform.position, Quaternion.identity);

        m_ListBullet.Add(bullet);
        return bullet;
    }

    Vector2 GetVeclocity()
    {
        //need implement
        Vector2 veclocity;

        veclocity.x = 0;// M_BULLET_SPEED * Player.Instance.transform.position.x;
        veclocity.y = M_BULLET_SPEED;// * Player.Instance.transform.position.y;

        return veclocity;
    }
}
