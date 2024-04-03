using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TouchingDirections))]
public class Knight : MonoBehaviour
{
    [Tooltip("Determines the walkspeed of the knight")]
    public float WalkSpeed = 3f;

    Rigidbody2D _rb;
    TouchingDirections _touchingDirections;

    public enum WalkableDirection { Right, Left };

    Vector2 _walkDirectionVector = Vector2.right;
    WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if(_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    _walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    _walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_touchingDirections.IsGrounded && _touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        _rb.velocity = new Vector2(WalkSpeed * _walkDirectionVector.x, _rb.velocity.y);
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("'WalkDirection' not set to legal values of Left or Right");
        }
    }
}
