using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dpad : MonoBehaviour
{
    [SerializeField]
    private float radius = 3.0f;
    [SerializeField]
    private float dpadUnit = 1.0f;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 GetAxis()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 axisVector = touchPos - transform.position;
            if (axisVector.magnitude > radius)
            {
            }
            else
            {
                return axisVector / radius * dpadUnit;
            }
        }
        return Vector2.zero;
    }
}
