using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update ()
    {
        transform.position = new Vector3(_target.position.x, transform.position.y, transform.position.z);
    }
}
