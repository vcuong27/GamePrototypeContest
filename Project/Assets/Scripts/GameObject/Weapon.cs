using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    public bool Ready;
    public bool OutOfAmmo => nextBullet >= magSize;



    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float rateOfFire;
    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private int magSize;
    [SerializeField]
    private int overheatBuildUp;
    [SerializeField]
    private int overheat;
    [SerializeField]
    private int overheatMax;
    [SerializeField]
    private Bullet bullet;

    private bool reloading;
    private float reloadTimer;
    private float nextFire;
    [SerializeField]
    private bool firing;

    private List<Bullet> bullets = new List<Bullet>();
    private int nextBullet;

    // Start is called before the first frame update
    void Start()
    {
        nextBullet = 0;
        for (int i = 0; i < magSize; i++)
        {
            Bullet newBullet = Instantiate(bullet);
            newBullet.enabled = false;
            newBullet.GetComponent<Moveable>().StopOnArrival = false;
            newBullet.GetComponent<Moveable>().linearspeed = true;
            bullets.Add(newBullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OutOfAmmo)
        {
            if (reloadTimer < Time.time && !reloading)
            {
                nextBullet = 0;
            }
        }
        else if (firing)
        {
            if (nextFire < Time.time)
            {
                if (!OutOfAmmo)
                {
                    bullets[nextBullet].transform.position = transform.position;
                    bullets[nextBullet].enabled = true;
                    bullets[nextBullet].GetComponent<Moveable>().MoveTo(transform.up);
                    bullets[nextBullet].GetComponent<Moveable>().LookAt(transform.up);
                }
                else
                {
                    reloading = true;
                }
            }
        }
    }

    internal void Fire()
    {
        firing = true;
        nextFire = Time.time + 1 / rateOfFire;
    }

    private void OnDisable()
    {
        reloading = false;
    }

    private void OnEnable()
    {
        reloading = true;
    }
}
