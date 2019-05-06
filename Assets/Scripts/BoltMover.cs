﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMover : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
}