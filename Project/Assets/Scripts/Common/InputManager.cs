using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Default;
    enum ControllerType
    {
        KeyBoard,
        Touch,
        None
    }

    public delegate void KeyHandler(KeyCode k, float dt);
    public delegate void DpadHandler(Vector2 axis, float dt);
    public delegate void TouchInputHandler(Vector2 position, float dt);

    private List<KeyCode> holding = new List<KeyCode>();
    public KeyHandler OnKeyPressed;
    public DpadHandler OnDpadPressed;
    public TouchInputHandler OnTouch;

    private ControllerType type = ControllerType.None;

    public Dpad dpad;

    // TODO: GameSetting Implement later
    public bool dpadEnabled = false;
    public bool touchInputEnabled = false;

    private InputManager()
    {

    }

    ~InputManager()
    {
        OnKeyPressed = null;
        OnDpadPressed = null;
        OnTouch = null;
    }

    private void Awake()
    {
        Default = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            type = ControllerType.Touch;
        }

        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            type = ControllerType.KeyBoard;
        }
        // TODO: debug

        //type = ControllerType.Touch;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (type == ControllerType.KeyBoard)
        {
            KeyboardHandle(dt);
        }
        else if (type == ControllerType.Touch)
        {
            if (dpad != null)
            {
                DpadHandle(dt);
            }
        }
    }

    void KeyboardHandle(float dt)
    {

        //Input Handling
        if (Input.anyKey)
        {
            foreach (KeyCode k in Input.inputString.ToCharArray())
            {
                holding.Add(k);
            }
        }
        else
        {
            OnKeyPressed?.Invoke(KeyCode.None, dt);
        }

        foreach (KeyCode k in holding.ToArray())
        {
            if (!Input.GetKey(k)) holding.Remove(k);
            else OnKeyPressed?.Invoke(k, dt);
        }
    }

    void DpadHandle(float dt)
    {
        if (dpad == null) return;
        OnDpadPressed?.Invoke(dpad.GetAxis(), dt);
    }

    void TouchHandle(float dt)
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        OnTouch?.Invoke(touchPos, dt);
    }
}
