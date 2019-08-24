using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private DamageComponent damageComponent;
    private Moveable moveable;

    // Start is called before the first frame update
    void Start()
    {
        damageComponent = GetComponent<DamageComponent>();
        moveable = GetComponent<Moveable>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    void Explode()
    {
        //play explode vfx

    }
}
