using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MeleeWeapon : MonoBehaviour
{
    public Vector3 targetVector;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float baseAttackTime;
    public float FireRate => 1 / baseAttackTime;

    private float reloadTimer;
    private float nextFire = float.PositiveInfinity;
    [SerializeField]
    private bool attacking;

    private List<Bullet> bullets = new List<Bullet>();
    private int nextBullet;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            while (nextFire < Time.time)
            {
                bullets[nextBullet].transform.position = transform.position;
                bullets[nextBullet].transform.up = targetVector;
                //Debug.Log($"bulet {bullets[nextBullet].name} enabled: {bullets[nextBullet].enabled} ");
                bullets[nextBullet].gameObject.SetActive(true);
                nextFire += FireRate;
                nextBullet++;
            }
        }

        if (!attacking)
        {

            animator.SetBool("trigger", false);
        }
    }

    public void Attack()
    {
        if (attacking) return;
        animator.SetBool("trigger", true);
        attacking = true;
        nextFire = Time.time + FireRate;
    }

    public void StopAttack()
    {
        attacking = false;
    }


    private void HitScan()
    {
        //TODO; hitscan with melee
    }
}
