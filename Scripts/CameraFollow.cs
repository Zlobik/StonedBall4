﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _offset = new Vector3(0, 0, -10);

    private void Update ()
    {
        if (transform.position != _target.position + _offset)
        {
            transform.position = new Vector3(_target.position.x, 0, _offset.z);
        }
    }
}
