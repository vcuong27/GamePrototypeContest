using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public delegate void MouseHandler(int button, Vector3 position, float dt);
    public delegate void DpadHandler(Vector2 axis, float dt);
    public delegate void TouchInputHandler(Vector2 pos, float dt);

    private HashSet<KeyCode> holding = new HashSet<KeyCode>();
    private HashSet<int> mouseHolding = new HashSet<int>();
    public KeyHandler OnKeyHold;
    public MouseHandler OnMouseHold;
    public DpadHandler OnDpadHold;
    public TouchInputHandler OnTouch;
    [SerializeField]
    private ControllerType type = ControllerType.None;

    public Dpad dpad;

    // TODO: GameSetting Implement later
    public bool dpadEnabled = false;
    public bool touchInputEnabled = true;

    // Debug
    public Text debug => GameManager.Instance.debug2;
    private InputManager()
    {

    }

    ~InputManager()
    {
        OnKeyHold = null;
        OnDpadHold = null;
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

    private void OnGUI()
    {
        float dt = Time.deltaTime;
        if (type == ControllerType.KeyBoard)
        {
            KeyboardHandle(dt);
        }

        if (type == ControllerType.Touch)
        {
            if (dpad != null && dpadEnabled)
            {
                DpadHandle(dt);
            }
            if (touchInputEnabled)
            {
                if (Input.touchCount > 0)
                {
                    TouchHandle(dt);
                }
            }
        }

        // Debug
        string debugText = "";
        foreach (KeyCode k in holding)
        {
            debugText += k.ToString();
        }
        foreach (int k in mouseHolding)
        {
            debugText += "mouse" + k.ToString();
        }
        //debugText += "touch" + Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).ToString();
        debug.text = debugText;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void KeyboardHandle(float dt)
    {
        Event e = Event.current;

        if (e.isKey)
        {
            if (e.type == EventType.KeyDown)
            {
                holding.Add(e.keyCode);
            }
            else if (e.type == EventType.KeyUp)
            {
                holding.Remove(e.keyCode);
            }
        }
        if (e.isMouse)
        {
            if (e.type == EventType.MouseDown)
            {
                mouseHolding.Add(e.button);
            }
            else if (e.type == EventType.MouseUp)
            {
                mouseHolding.Remove(e.button);
            }
        }
        foreach (KeyCode k in holding)
        {
            OnKeyHold?.Invoke(k, dt);
        }
        foreach (int button in mouseHolding)
        {
            OnMouseHold?.Invoke(button, Input.mousePosition, dt);
        }
    }

    void DpadHandle(float dt)
    {
        if (dpad == null) return;
        OnDpadHold?.Invoke(dpad.GetAxis(), dt);
    }

    void TouchHandle(float dt)
    {
        if (Input.touchCount > 0)
            OnTouch?.Invoke(Input.GetTouch(0).position, dt);
    }
}
