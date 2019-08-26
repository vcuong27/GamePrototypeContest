using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Engage,
        Attack,
        Flee,
        Die = -1
    }

    public Player Target => GameManager.Instance.players[0];
    private Moveable moveable;
    private Destructible heath;
    private HeathBarUI BossHeathBar => boss ? GameManager.Instance.bossHeathBarUI : null;
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private bool boss = false;
    [SerializeField]
    private bool lockAngle = false;
    [SerializeField]
    private bool IsLockAngle = false;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private float attackInterval;

    [SerializeField]
    private float fleeRange;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float detectRange;
    private float DetectRange
    {
        get
        {
            return detectRange * (((heath.HP - heath.MaxHP) < 0) ? 2 : 1);
        }
    }
    [SerializeField]
    private float outOfContactRange;
    private float OutOfContactRange
    {
        get
        {
            return outOfContactRange * (((heath.HP - heath.MaxHP) < 0) ? 2 : 1);
        }
    }
    [SerializeField]
    private EnemyState state = EnemyState.Idle;
    //
    private Animator animator;


    // 
    private float TargetDistance => TargetVector.magnitude;

    private Vector2 TargetVector => Target != null ? (Vector2)(Target.transform.position - transform.position) : Vector2.up;
    private float TargetAngle => Vector2.SignedAngle(Target.transform.position - transform.position, transform.up);
    private Vector2 Position => transform.position;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        moveable = GetComponent<Moveable>();
        animator = GetComponent<Animator>();
        heath = GetComponent<Destructible>();
        if (heath) heath.OnDestruct += OnDestruct;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossHeathBar)
            BossHeathBar.percentage = heath.HP / heath.MaxHP;
        if (weapon)
            weapon.targetVector = TargetVector;
        if (moveable && Target != null && !Target.isActiveAndEnabled)
            moveable.target = null;

        if (moveable && Target)
        {
            moveable.target = Target.transform;
        }
        else
        {
            if (weapon)
                weapon.StopFire();
            moveable.Stop();
            state = EnemyState.Idle;
            animator.SetInteger("state", (int)state);
            return;
        }
        if (moveable && IsLockAngle) moveable.target = null;

        if (TargetDistance < fleeRange)
        {
            state = EnemyState.Flee;
            Flee();

        }
        else if (TargetDistance < attackRange && weapon != null && weapon.Ready)
        {
            if (lockAngle)
            {
                IsLockAngle = true;
            }
            state = EnemyState.Attack;
            Attack();
        }
        else if (TargetDistance < attackRange)
        {
            //
        }
        else if (TargetDistance < DetectRange)
        {
            if (weapon)
                weapon.StopFire();
            state = EnemyState.Engage;
            Engage();
        }
        else if (TargetDistance < OutOfContactRange && state == EnemyState.Engage)
        {
            if (weapon)
                weapon.StopFire();
            state = EnemyState.Engage;
            Engage();
        }
        else
        {
            if (weapon)
                weapon.StopFire();
            moveable.Stop();
            state = EnemyState.Idle;
        }

        animator.SetInteger("state", (int)state);
    }


    private void Engage()
    {
        Vector2 engagePosition = TargetVector * (TargetDistance - attackRange + 0.5f) + Position;
        moveable.MoveTo(engagePosition);
    }

    private void Attack()
    {
        weapon.Fire();
    }

    private void Flee()
    {
        Vector2 fleePosition = TargetVector * (TargetDistance - attackRange) + Position;
        moveable.MoveTo(fleePosition);
    }

    private IEnumerator OnDestruct(Destructible destructible)
    {
        yield return new WaitForSeconds(1);
    }

}
