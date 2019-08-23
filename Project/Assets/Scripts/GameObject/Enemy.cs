using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamagingComponent
{
    enum EnemyState
    {
        Idle,
        Engage,
        Attack,
        Flee,
        Die
    }

    [SerializeField]
    private Player target;
    private Moveable moveable;


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
    [SerializeField]
    private float outOfContactRange;
    [SerializeField]
    private EnemyState state = EnemyState.Idle;
    //
    private Animator animator;


    // 
    private float TargetDistance => DistanceVector.magnitude;
    private Vector2 DistanceVector => (Vector2)(target.transform.position - transform.position);
    private float TargetAngle => Vector2.SignedAngle(target.transform.position - transform.position, transform.up);
    private Vector2 Position => transform.position;


    // Start is called before the first frame update
    void Start()
    {
        moveable = GetComponent<Moveable>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetDistance < fleeRange)
        {
            state = EnemyState.Flee;
            Flee();

        }
        else if (TargetDistance < attackRange)
        {
            state = EnemyState.Attack;
            Attack();
        }
        else if (TargetDistance < outOfContactRange)
        {
            state = EnemyState.Engage;
            Engage();
        }
        else
        {
            state = EnemyState.Idle;
        }

        animator.SetInteger(0, (int)state);
    }


    private void Engage()
    {
        Vector2 engagePosition = DistanceVector * (TargetDistance - attackRange) + Position;
        moveable.MoveTo(engagePosition);
    }

    private void Attack()
    {
    }

    private void Flee()
    {
        Vector2 fleePosition = DistanceVector * (TargetDistance - attackRange) + Position;
        moveable.MoveTo(fleePosition);
    }
}
