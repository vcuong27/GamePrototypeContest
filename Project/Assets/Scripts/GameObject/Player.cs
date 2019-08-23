﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Destructible
{
    // Keybinding
    public const KeyCode KEY_MOVE_LEFT = KeyCode.A;
    public const KeyCode KEY_MOVE_RIGHT = KeyCode.D;
    public const KeyCode KEY_MOVE_UP = KeyCode.W;
    public const KeyCode KEY_MOVE_DOWN = KeyCode.S;

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
        InputManager.Default.OnKeyHold += OnKeyHeld;
        InputManager.Default.OnMouseHold += OnMouseHeld;
        InputManager.Default.OnDpadHold += OnDpadHold;
        InputManager.Default.OnTouch += OnTouch;

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


    private void OnKeyHeld(KeyCode k, float dt)
    {
        switch (k)
        {
            case KEY_MOVE_UP:
                if (!moveable.strafe)
                {
                    moveable.input += (Vector2)transform.up;
                }
                else
                    moveable.input += Vector2.up;
                break;
            case KEY_MOVE_DOWN:
                if (!moveable.strafe)
                {
                    moveable.input += (Vector2)transform.up;
                }
                else
                    moveable.input += Vector2.down;
                break;
            case KEY_MOVE_LEFT:
                if (!moveable.strafe)
                {
                    moveable.TurnLeft(true, dt);
                }
                else
                    moveable.input += Vector2.left;
                break;
            case KEY_MOVE_RIGHT:
                if (!moveable.strafe)
                {
                    moveable.TurnLeft(false, dt);
                }
                else
                    moveable.input += Vector2.right;
                break;
            case KeyCode.None:
                break;
            default:
                break;
        }
        if (moveable.input.magnitude > 0 && k != KeyCode.None)
        {
            moveable.destination = null;
        }
        //Debug.Log($"Key {k.ToString()} pressed");
    }
    private void OnDpadHold(Vector2 input, float dt)
    {
        moveable.input = input;
    }
    public void OnTouch(Touch touch, float dt)
    {
        moveable.MoveTo(Camera.main.ScreenToWorldPoint(touch.position));
    }

    void OnMouseHeld(int button, Vector3 position, float dt)
    {
        if (button == 0)
        {
            moveable.MoveTo(Camera.main.ScreenToWorldPoint(position));
        }
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
