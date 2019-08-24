using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    public bool Ready => !OutOfAmmo && !Reloading;
    public bool OutOfAmmo => nextBullet >= magSize;
    public int BulletRemains => magSize - nextBullet;



    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float rateOfFire;
    public float FireRate => 60 / rateOfFire;
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
    [SerializeField]
    private bool reloading = false;
    public bool Reloading
    {
        get
        {
            return reloading;
        }
        set
        {
            if (value == reloading) return;
            if (value)
            {
                reloadTimer = Time.time + reloadTime;
            }
            else reloadTimer = Mathf.Infinity;
            reloading = value;
        }
    }
    private float reloadTimer;
    private float nextFire = float.PositiveInfinity;
    [SerializeField]
    private bool firing;
    public bool Firing => firing;

    private List<Bullet> bullets = new List<Bullet>();
    private int nextBullet;

    private Animator muzzleflash;

    // Start is called before the first frame update
    void Start()
    {
        muzzleflash = GetComponent<Animator>();
        nextBullet = 0;
        for (int i = 0; i < magSize; i++)
        {
            //Bullet newBullet = Instantiate(bullet);
            //newBullet.enabled = false;
            //newBullet.GetComponent<Moveable>().StopOnArrival = false;
            //newBullet.GetComponent<Moveable>().linearspeed = true;
            //bullets.Add(newBullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadTimer < Time.time && Reloading)
        {
            nextBullet = 0;
            Reloading = false;
        }

        if (OutOfAmmo)
        {
            Reloading = true;
            firing = false;
        }
        else if (firing)
        {
            while (nextFire < Time.time)
            {
                //bullets[nextBullet].transform.position = transform.position;
                //bullets[nextBullet].enabled = true;
                //bullets[nextBullet].GetComponent<Moveable>().MoveTo(transform.up);
                //bullets[nextBullet].GetComponent<Moveable>().LookAt(transform.up);
                nextFire += FireRate;
                nextBullet++;
            }
        }

        if (!firing)
        {

            muzzleflash.SetBool("trigger", false);
        }
    }

    public void Fire()
    {
        if (firing) return;
        muzzleflash.SetBool("trigger", true);
        firing = true;
        nextFire = Time.time + FireRate;
    }

    public void StopFire()
    {
        firing = false;
    }

    private void OnDisable()
    {
        Reloading = false;
    }

    private void OnEnable()
    {
        if (OutOfAmmo)
            Reloading = true;
    }
}
