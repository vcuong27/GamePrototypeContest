using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float muzzleVelocity;
    public float MuzzleVelocity { get => muzzleVelocity / 10; }
    [SerializeField]
    private float delay;
    [SerializeField]
    private float acelleration;
    [SerializeField]
    private float drag;
    [SerializeField]
    private float LifeTime = 2;
    private float firePoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (firePoint > Time.time) return;
        float dt = Time.deltaTime;
        transform.position += transform.up.normalized * MuzzleVelocity * dt;
        muzzleVelocity -= drag * dt;
        if (firePoint + LifeTime < Time.time)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null && tag == "ProjectilePlayer")
            Explode();
        if (collision.GetComponent<Enemy>() == null && tag == "ProjectileEnemy")
            Explode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void OnEnable()
    {
        firePoint = Time.time + delay;
        //Debug.Log($"OnEnable {name}");

    }

    private void OnDisable()
    {
        //Debug.Log($"OnDisable {name}");
    }

    void Explode()
    {
        //play explode vfx
        gameObject.SetActive(false);
    }
}
