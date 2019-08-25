using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float muzzleVelocity;
    public float MuzzleVelocity { get => muzzleVelocity / 10; }
    [SerializeField]
    private float drag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        transform.position += transform.up.normalized * MuzzleVelocity * dt;
        muzzleVelocity -= drag * dt;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void OnEnable()
    {
        //Debug.Log($"OnEnable {name}");
    }

    private void OnDisable()
    {
        //Debug.Log($"OnDisable {name}");
    }

    void Explode()
    {
        //play explode vfx
        enabled = false;
    }
}
