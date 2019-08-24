using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private DamageComponent m_DamageComponent;

    private Vector2 m_Veclocity;
    private void Start()
    {
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        //moving 
        Moving(dt);

        //destroy
        if ((transform.position.x < -GameManager.Instance.m_ScreenWidth / 2) || (transform.position.x > GameManager.Instance.m_ScreenWidth / 2)
            || (transform.position.y < -GameManager.Instance.m_ScreenHeight / 2) || (transform.position.y > GameManager.Instance.m_ScreenHeight / 2))
        {
            Explorer();
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetVelocity(Vector2 veclocity)
    {
        m_Veclocity = veclocity;
    }

    public void SetDamageComponent(DamageComponent damage)
    {
        m_DamageComponent = damage;
    }

    public DamageComponent GetDamageComponent()
    {
        return m_DamageComponent;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void Moving(float dt)
    {
        transform.position += new Vector3(m_Veclocity.x * dt, m_Veclocity.y * dt, 0f);
    }

    private void Explorer()
    {
        SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: need implement collision 
        //check collision with something to destroy this for vfx
    }

}
