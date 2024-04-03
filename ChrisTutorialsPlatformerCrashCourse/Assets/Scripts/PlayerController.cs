using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    bool _isMoving = false;
    [SerializeField]
    bool _isRunning = false;
    bool _isFacingRight = true;

    Vector2 _moveInput;
    Rigidbody2D _rb;
    Animator _animator;
    TouchingDirections _touchingDirections;

    [Tooltip("Determines the walk-speed of the player.")]
    public float WalkSpeed = 5f;
    [Tooltip("Determines the run-speed of the player.")]
    public float RunSpeed = 8f;
    [Tooltip("Determines the strength of the jump.")]
    public float JumpImpulse = 10f;
    [Tooltip("Determines the lateral move-speed of the player while in the air.")]
    public float AirwalkSpeed = 3f;

    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            _animator.SetBool(AnimationStrings.IsMoving, value);
        }
    }

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            _isRunning = value;
            _animator.SetBool(AnimationStrings.IsRunning, value);
        }
    }

    public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

    float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !_touchingDirections.IsOnWall)
                {
                    if (_touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return RunSpeed;
                        }
                        else
                        {
                            return WalkSpeed;
                        }
                    }
                    else
                    {
                        // TODO: Keep initial lateral velocity on jump. When switching directions, then keep using AirwalkSpeed.
                        // We're in the air:
                        return AirwalkSpeed;
                    }
                }
                else
                {
                    // idle speed:
                    return 0;
                }
            }
            else
            {
                // Player is not allowed to move:
                return 0;
            }
        }
    }

    bool IsFacingRight
    {
        get => _isFacingRight;
        set
        {
            if(_isFacingRight != value)
            {
                // Flip x-orientation:
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    bool IsAlive => _animator.GetBool(AnimationStrings.IsAlive);

    void Awake()
    {
        Debug.Assert(WalkSpeed > 0f, "'walkSpeed' must be greater than 0!");
        Debug.Assert(RunSpeed > 0f, "'RunSpeed' must be greater than 0!");
        Debug.Assert(WalkSpeed < RunSpeed, "'RunSpeed' must be greater than 'WalkSpeed'!");

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Only control lateral movement from Player Input:
        _rb.velocity = new Vector2(
            _moveInput.x * CurrentMoveSpeed, 
            _rb.velocity.y // Intentionally not influencing vertical movement of `rb`
        );

        _animator.SetFloat(AnimationStrings.YVelocity, _rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = _moveInput != Vector2.zero;

            SetFacingDirection(_moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO: Check if alive
        if (CanMove)
        {
            if (context.started &&
                _touchingDirections.IsGrounded)
            {
                _animator.SetTrigger(AnimationStrings.JumpTrigger);
                _rb.velocity = new Vector2(_rb.velocity.x, JumpImpulse);
            }
            // Allow for 'short hopping' on release:
            if (context.canceled && _rb.velocity.y > 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // TODO: Is attack is pressed in mid air, the hero character will attack the moment he hits the ground.
        // This feels akward.
        // TODO: The Knight enemy keeps some of his forward momentum while attacking. The player does not. Perhaps we should make that consistent?
        if (context.started)
        {
            _animator.SetTrigger(AnimationStrings.AttackTrigger);
        }
    }
}
