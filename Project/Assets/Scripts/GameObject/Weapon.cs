using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    public bool Ready;
    private Transform target;

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


    private float reloadTimer;
    private float nextFire;
    [SerializeField]
    private bool firing;

    private List<Bullet> magazine = new List<Bullet>();
    private bool[] availableBullets;

    // Start is called before the first frame update
    void Start()
    {
        availableBullets = new bool[magSize];
        for (int i = 0; i < magSize; i++)
        {
            Bullet newBullet = Instantiate<Bullet>(bullet);
            newBullet.enabled = false;
            magazine.Add(newBullet);
            availableBullets[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firing)
        {
            int? nextBullet = GetBulletAvaiable();
            if (nextFire < Time.time && nextBullet != null)
            {
                magazine[nextBullet.Value].transform.position = transform.position;
                magazine[nextBullet.Value].enabled = true;
                magazine[nextBullet.Value].GetComponent<Moveable>().target = target;
            }
        }
    }

    internal void Fire()
    {
        firing = true;
        nextFire = Time.time + 1 / rateOfFire;
    }

    private int? GetBulletAvaiable()
    {
        return 0;
    }
}
