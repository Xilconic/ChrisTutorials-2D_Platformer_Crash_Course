using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    bool _isMoving = false;
    [SerializeField]
    bool _isRunning = false;

    Vector2 _moveInput;
    Rigidbody2D _rb;
    Animator _animator;

    [Tooltip("Determines the walk-speed of the player.")]
    public float WalkSpeed = 5f;
    
    public bool IsMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            _animator.SetBool("isMoving", value);
        }
    }

    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            _isRunning = value;
            _animator.SetBool("isRunning", value);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(WalkSpeed > 0f, "'walkSpeed' must be greater than 0!");
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Only control lateral movement from Player Input:
        _rb.velocity = new Vector2(
            _moveInput.x * WalkSpeed, 
            _rb.velocity.y // Intentionally not influencing vertical movement of `rb`
        );
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        IsMoving = _moveInput != Vector2.zero;
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
}
