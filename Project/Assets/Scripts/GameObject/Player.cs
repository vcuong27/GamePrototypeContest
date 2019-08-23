using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Destructible
{

    enum PlayerState
    {
        Idle,
        SwapWeapon,
        Reload,
        Attack,
        Die = -1
    }

    [SerializeField]
    private Enemy target;
    public Enemy Target
    {
        get { return target; }
        set
        {
            target = value;
            moveable.target = target.transform;
        }
    }
    private Moveable moveable;
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackInterval;

    [SerializeField]
    private PlayerState state = PlayerState.Idle;
    //
    private Animator body;
    private Animator feet;
    


    // 
    private float TargetDistance => TargetVector.magnitude;
    private Vector2 TargetVector => (target.transform.position - transform.position);
    private float TargetAngle => Vector2.SignedAngle(target.transform.position - transform.position, transform.up);
    private Vector2 Position => transform.position;


    // Start is called before the first frame update
    void Start()
    {
        moveable = GetComponent<Moveable>();
        body = GetComponent<Animator>();
        feet = GetComponentsInChildren<Animator>()[1];
        Target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == PlayerState.Die)
        {
            body.SetInteger("state", (int)state);
            return;
        }

        if (TargetDistance < attackRange && weapon.Ready)
        {
            state = PlayerState.Attack;
            Attack();
        }
        else
        {
            state = PlayerState.Idle;
        }
        feet.SetInteger("state", (int)moveable.State);
        body.SetInteger("state", (int)state);
    }

    private void Reload()
    {

    }

    private void Attack()
    {
        weapon.Fire();
    }

    protected override void Destruct()
    {
        state = PlayerState.Die;
        base.Destruct();
    }
}
