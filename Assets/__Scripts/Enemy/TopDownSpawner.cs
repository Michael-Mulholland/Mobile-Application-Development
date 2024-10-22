﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownSpawner : MonoBehaviour
{
    // == private fields ==
    private float speed = 5.0f;

    private Rigidbody2D rb;

    // == private methods ==
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, -1 * speed);
    }
}
