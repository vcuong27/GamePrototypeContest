using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moveable : MonoBehaviour
{
    public delegate Vector2 GetControllerInput();

    public static readonly float HIT_WALL_DAZED_TIME = 0.3f;

    // Keybinding
    public const KeyCode KEY_MOVE_LEFT = KeyCode.A;
    public const KeyCode KEY_MOVE_RIGHT = KeyCode.D;
    public const KeyCode KEY_MOVE_UP = KeyCode.W;
    public const KeyCode KEY_MOVE_DOWN = KeyCode.S;

    // Moving 
    [SerializeField]
    private float aceleration;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float deceleration;
    private float dazedTimer;
    private float Dazed
    {
        get { return dazedTimer - Time.time; }
        set
        {
            dazedTimer = Time.time + value;
        }
    }

    // Touch/AI
    [SerializeField]
    private bool autopilot = true;
    [SerializeField]
    private bool linearspeed = false;

    // Turning and aiming
    public Vector3 destination;
    public Transform target;
    [SerializeField]
    private bool onlyForward; // Either move vector lineup with facing or not
    public Vector2 onward => transform.up;

    [SerializeField]
    private float turnRate = 21600; // 21600 degree per sec = 360 degree per frame

    // Controller/TouchPad
    public GetControllerInput getControllerInput;

    [SerializeField]
    private Vector2 velocity = Vector2.zero;


    // DEbug


    public Text debug;


    // Start is called before the first frame update
    void Start()
    {

        InputManager.Default.OnKeyPressed += OnKeyPressed;
        InputManager.Default.OnDpadPressed += OnDpadPressed;
    }

    // Update is called once per frame
    void Update()
    {
        //stick to the mouse
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dt = Time.deltaTime;
        // Turning

        float targetAngle = 0;
        if (target != null)
        {
            targetAngle = Vector2.SignedAngle(onward, target.position - transform.position);
        }
        else if (destination != Vector3.zero)
        {
            targetAngle = Vector2.SignedAngle(onward, destination - transform.position);
        }

        if (targetAngle != 0f)
        {
            float maximumAngle = turnRate * dt;
            if (maximumAngle > Mathf.Abs(targetAngle))
            {
                transform.LookAt2D(target != null ? target.position : destination);//snap
            }
            else
            {
                Turn(maximumAngle * targetAngle / Mathf.Abs(targetAngle));
            }

        }

        // Moving
        if (Mathf.Abs((destination - transform.position).magnitude) < 0.1f && autopilot == true)
        {
            autopilot = false;
            Stop();
        }

        if (autopilot)
        {
            Acelerate(onward, dt);
        }
        transform.position += new Vector3(velocity.x * dt, velocity.y * dt, 0f);
        debug.text = $"Velocity {velocity.ToString()}";


        // Object always want to rest
        Decelerate(dt);
    }

    private void OnKeyPressed(KeyCode k, float dt)
    {
        switch (k)
        {
            case KEY_MOVE_UP:
                Acelerate(Vector2.up, dt);
                break;
            case KEY_MOVE_DOWN:
                Acelerate(Vector2.down, dt);
                break;
            case KEY_MOVE_LEFT:
                if (onlyForward)
                {
                    Turn(turnRate / 60);
                }
                else
                    Acelerate(Vector2.left, dt);
                break;
            case KEY_MOVE_RIGHT:
                if (onlyForward)
                {
                    Turn(-turnRate, dt);
                }
                else
                    Acelerate(Vector2.right, dt);
                break;
            case KeyCode.None:
                break;
            default:
                break;
        }
        //Debug.Log($"Key {k.ToString()} pressed");
    }
    private void OnDpadPressed(Vector2 input, float dt)
    {
        if (input == Vector2.zero)
        {
            return;
        }

        Acelerate(input, dt);
    }

    // Moving
    private void Acelerate(Vector2 input, float dt)
    {
        if (dazedTimer > Time.time) return;

        if (onlyForward)
        {
            input = onward;
        }
        if (autopilot)
        {
            if (linearspeed)
            {
                velocity += input.normalized * aceleration * dt;
                if (velocity.magnitude > maxVelocity)
                {
                    velocity = velocity.normalized * maxVelocity;
                }
            }
            else
            {
                // v^2 = v0^2 + 2a * displacement
                // v = 0 to stop
                float minimumDisplacement = -velocity.magnitude * velocity.magnitude / 2 / deceleration;
                float currentDistance = -1;
                if (target != null)
                {
                    currentDistance = (target.position - transform.position).magnitude;
                }
                else if (destination != null)
                {
                    currentDistance = (destination - transform.position).magnitude;
                }

                if (Mathf.Abs(currentDistance) <= Mathf.Abs(minimumDisplacement))
                {
                }
                else
                {
                    velocity += input.normalized * aceleration * dt;
                    if (velocity.magnitude > maxVelocity)
                    {
                        velocity = velocity.normalized * maxVelocity;
                    }
                }
            }
        }
        else
        {
            if (input != Vector2.zero)
            {
                velocity += input.normalized * aceleration * dt;
                if (velocity.magnitude > maxVelocity)
                {
                    velocity = velocity.normalized * maxVelocity;
                }
            }
        }
    }

    private void Decelerate(float dt)
    {
        float speedXDeceleration = velocity.x == 0 ? 0 : velocity.x / Mathf.Abs(velocity.x) * deceleration * dt;
        float speedYDeceleration = velocity.y == 0 ? 0 : velocity.y / Mathf.Abs(velocity.y) * deceleration * dt;

        if (Mathf.Abs(velocity.x) - Mathf.Abs(speedXDeceleration) <= 0)
            velocity.x = 0;
        else
            velocity.x -= speedXDeceleration;

        if (Mathf.Abs(velocity.y) - Mathf.Abs(speedYDeceleration) <= 0)
            velocity.y = 0;
        else
            velocity.y -= speedYDeceleration;
    }

    private void Stop()
    {
        velocity = Vector2.zero;
    }

    public void MoveTo(Vector2 destination)
    {
        this.destination = destination;
        autopilot = true;
    }

    public void Chase(Transform target)
    {
        this.target = target;
    }

    // Turning
    // We just do the z rotation as the game is 2d at XY
    public void Turn(float eulerAngle)
    {
        transform.Rotate(Vector3.forward, eulerAngle.SignedEulerAngle());
    }

    public void Turn(float eulerAngle, float dt)
    {
        Debug.Log($"eulerAngle {eulerAngle * dt}");
        Turn(eulerAngle * dt);
    }

    // Callbacks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "stun")
        {
            Dazed = HIT_WALL_DAZED_TIME;//TODO: stun factor
            Stop();
        }
        if (collision.transform.tag == "wall")
        {
            velocity = -transform.position.normalized;
            Dazed = HIT_WALL_DAZED_TIME;
        }
    }
}
