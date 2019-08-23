using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moveable : MonoBehaviour
{
    public enum MovingState
    {
        Stop,
        forward,
        left,
        right
    }

    public delegate Vector2 GetControllerInput();

    public static readonly float HIT_WALL_DAZED_TIME = 0.3f;

    // Keybinding
    public const KeyCode KEY_MOVE_LEFT = KeyCode.A;
    public const KeyCode KEY_MOVE_RIGHT = KeyCode.D;
    public const KeyCode KEY_MOVE_UP = KeyCode.W;
    public const KeyCode KEY_MOVE_DOWN = KeyCode.S;

    // Moving 
    [SerializeField]
    private Vector2 input;
    [SerializeField]
    private float aceleration;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float deceleration;
    [SerializeField]
    private float acceptableStopDistance = 0.3f;
    [SerializeField]
    private float turningMaxSpeedRatio = 0.7f;
    [SerializeField]
    private float dazedTimer;
    public float MaxVelocity
    {
        get
        {
            if (Turning && moveForward)
            {
                return maxVelocity * Mathf.Pow((360 - Mathf.Abs(DestinationAngle)) / 360, 3) * turningMaxSpeedRatio;
            }
            return maxVelocity;
        }
    }
    public float TargetDistance
    {
        get
        {
            float magnitude = 0;
            if (target != null)
            {
                magnitude = (target.position - transform.position).magnitude;
            }
            return magnitude;
        }
    }
    public float DestinationDistance
    {
        get
        {
            float magnitude = 0;
            if (destination != null)
            {
                magnitude = (destination.Value - transform.position).magnitude;
            }
            return magnitude;
        }
    }
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
    [SerializeField]
    public Vector3? destination;
    [SerializeField]
    public Transform target;
    [SerializeField]
    private bool moveForward; // Either target vector lineup with destination or not
    [SerializeField]
    private bool strafe = true; // Either move vector lineup with facing or not
    [SerializeField]
    private float acceptableTargetAngle = 0.1f;
    public Vector2 onward => transform.up;
    public Vector2 Onward;
    public MovingState movingstate;
    public MovingState State
    {
        get
        {
            if (velocity.y != 0 && (Mathf.Abs(velocity.y) - Mathf.Abs(velocity.x)) >= 0)
                return MovingState.forward;
            if (velocity.x < 0)
                return MovingState.left;
            if (velocity.x > 0)
                return MovingState.right;
            return MovingState.Stop;
        }
    }
    private float TargetAngle
    {
        get
        {
            float targetAngle = 0;
            if (target != null)
            {
                targetAngle = Vector2.SignedAngle(onward, target.position - transform.position);
            }
            return targetAngle;

        }
    }

    private float DestinationAngle
    {
        get
        {
            float destinationAngle = 0;
            if (destination != null)
            {
                destinationAngle = Vector2.SignedAngle(onward, destination.Value - transform.position);
            }
            return destinationAngle;

        }
    }

    private Vector2 TargetVector
    {
        get
        {
            Vector2 vector;
            if (target != null)
            {
                vector = target.position - transform.position;
            }
            else
            {
                vector = Vector2.up;
            }
            return vector;
        }
    }

    private Vector2 DestinationVector
    {
        get
        {
            Vector2 vector;
            if (destination != null)
            {
                vector = destination.Value - transform.position;
            }
            else
            {
                vector = Vector2.up;
            }
            return vector;
        }
    }

    [SerializeField]
    private float turnRate = 21600;
    private float TurnRate
    {
        get
        {
            if (velocity.magnitude > 0)
            {
                //TODO
            }
            return turnRate;
        }
    }
    private bool Turning => Mathf.Abs(DestinationAngle) >= acceptableTargetAngle;
    public bool turning;

    // Controller/TouchPad
    public GetControllerInput getControllerInput;

    [SerializeField]
    private Vector2 velocity = Vector2.zero;


    // DEbug


    public Text debug;


    // Start is called before the first frame update
    void Start()
    {
        InputManager.Default.OnKeyHold += OnKeyHeld;
        InputManager.Default.OnMouseHold += OnMouseHeld;
        InputManager.Default.OnDpadHold += OnDpadHold;
        InputManager.Default.OnTouch += OnTouch;
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG
        movingstate = State;
        //stick to the mouse
        //destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        float dt = Time.deltaTime;

        turning = Turning;
        Onward = transform.up;
        // Turning
        if (target != null && !moveForward)
            LookAt(target.position, dt);
        if (destination != null && moveForward)
            LookAt(destination.Value, dt);

        // Moving
        if (DestinationDistance < acceptableStopDistance && autopilot == true)
        {
            autopilot = false;
            destination = null;
            Stop();
        }

        if (autopilot)
        {
            input = DestinationVector;
        }
        Acelerate(dt);
        // Object always want to rest
        Decelerate(dt);

        transform.position += new Vector3(velocity.x * dt, velocity.y * dt, 0f);
        debug.text = $"Velocity {velocity.ToString()}";
        input = Vector2.zero;
    }

    private void OnKeyHeld(KeyCode k, float dt)
    {
        switch (k)
        {
            case KEY_MOVE_UP:
                if (!strafe)
                {
                    input += onward;
                }
                else
                    input += Vector2.up;
                break;
            case KEY_MOVE_DOWN:
                if (!strafe)
                {
                    input += -onward;
                }
                else
                    input += Vector2.down;
                break;
            case KEY_MOVE_LEFT:
                if (!strafe)
                {
                    Turn(TurnRate * dt);
                }
                else
                    input += Vector2.left;
                break;
            case KEY_MOVE_RIGHT:
                if (!strafe)
                {
                    Turn(-TurnRate * dt);
                }
                else
                    input += Vector2.right;
                break;
            case KeyCode.None:
                break;
            default:
                break;
        }
        if (input.magnitude > 0 && k != KeyCode.None)
        {
            destination = null;
        }
        //Debug.Log($"Key {k.ToString()} pressed");
    }
    private void OnDpadHold(Vector2 input, float dt)
    {
        this.input = input;
    }
    public void OnTouch(Touch touch, float dt)
    {
        MoveTo(Camera.main.ScreenToWorldPoint(touch.position));
    }

    void OnMouseHeld(int button, Vector3 position, float dt)
    {
        if (button == 0)
        {
            MoveTo(Camera.main.ScreenToWorldPoint(position));
            LookAt(position);
        }
    }

    // Moving
    private void Acelerate(float dt)
    {
        if (dazedTimer > Time.time) return;

        if (autopilot)
        {
            if (linearspeed)
            {
                velocity = input.normalized * (velocity.magnitude + aceleration * dt);
                if (velocity.magnitude > MaxVelocity)
                {
                    velocity = velocity.normalized * MaxVelocity;
                }
            }
            else
            {
                // v^2 = v0^2 + 2a * displacement
                // v = 0 to stop
                float minimumDisplacement = velocity.magnitude * velocity.magnitude / 2 / deceleration;
                if (DestinationDistance <= minimumDisplacement)
                {
                    // decelerate
                    //Debug.Log("slowdown");
                }
                else
                {
                    //Debug.Log("speedup");
                    velocity = input.normalized * (velocity.magnitude + aceleration * dt);
                    if (velocity.magnitude > MaxVelocity)
                    {
                        velocity = velocity.normalized * MaxVelocity;
                    }
                }
            }
        }
        else
        {
            if (linearspeed)
            {
                velocity = input.normalized * MaxVelocity;
            }
            if (input != Vector2.zero)
            {
                velocity += input.normalized * aceleration * dt;
                if (velocity.magnitude > MaxVelocity)
                {
                    velocity = velocity.normalized * MaxVelocity;
                }
            }
        }
    }

    private void Decelerate(float dt)
    {
        if (input.x == 0)
        {
            //Debug.Log("decelerateX");
            float speedXDeceleration = velocity.x == 0 ? 0 : velocity.x / Mathf.Abs(velocity.x) * deceleration * dt;
            if (Mathf.Abs(velocity.x) - Mathf.Abs(speedXDeceleration) <= 0)
                velocity.x = 0;
            else
                velocity.x -= speedXDeceleration;
        }

        if (input.y == 0)
        {
            //Debug.Log("decelerateY");
            float speedYDeceleration = velocity.y == 0 ? 0 : velocity.y / Mathf.Abs(velocity.y) * deceleration * dt;

            if (Mathf.Abs(velocity.y) - Mathf.Abs(speedYDeceleration) <= 0)
                velocity.y = 0;
            else
                velocity.y -= speedYDeceleration;
        }
    }

    private void Stop()
    {
        autopilot = false;
        input = Vector2.zero;
        velocity = Vector2.zero;
    }

    public void MoveTo(Vector2 destination)
    {
        this.destination = destination;
        input = DestinationVector;
        autopilot = true;
    }

    public void LookAt(Vector2 targetPosition)
    {

    }
    public void LookAt(Vector2 targetPosition, float dt)
    {
        float targetAngle = 0;

        targetAngle = Vector2.SignedAngle(onward, targetPosition - (Vector2)transform.position);
        if (targetAngle != 0f)
        {
            float maximumAngle = TurnRate * dt;
            if (maximumAngle > Mathf.Abs(targetAngle))
            {
                transform.LookAt2D(targetPosition);//snap
                                                   //Debug.Log("snap");
            }
            else
            {
                //Debug.Log("turn");
                Turn(maximumAngle * targetAngle / Mathf.Abs(targetAngle));
            }

        }
    }
    public void Chase(Transform target)
    {
        this.target = target;
    }

    // Turning
    // We just do the z rotation as the game is 2d at XY
    public void Turn(float eulerAngle)
    {
        if (eulerAngle != 0)
            transform.Rotate(Vector3.forward, eulerAngle);
    }

    public void Turn(float eulerAngle, float dt)
    {
        if (eulerAngle != 0)
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
