﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : GameObject
{
    void Start()
    {
        base.Start();
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check collision with something
    }
}
