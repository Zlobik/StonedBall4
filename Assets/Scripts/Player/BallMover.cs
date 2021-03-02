using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpHeight;

    private bool _isGrounded = false;

    private void OnCollisionEnter2D (Collision2D collision)
    {
        _isGrounded = true;
    }

    private void Move (int horizontalDirection)
    {
        transform.position = new Vector3(transform.position.x + (horizontalDirection * _moveSpeed * Time.deltaTime), transform.position.y);
    }

    private void Update ()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Move(1);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Move(-1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                transform.DOMoveY(transform.position.y + _jumpHeight, _jumpDuration);
                _isGrounded = false;
            }
        }
    }
}
