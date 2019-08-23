using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Still contact with Destroyable but cannot receive damage
public class DamagingComponent : MonoBehaviour
{
    public float Damage => damage; // impact damage
    public float DOT => dot;// damage over time
    public float DOTDuration => dotDuration;
    public float DOTInterval => dotInterval;

    private float damage;
    private float dot;
    private float dotInterval;
    private float dotDuration;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
