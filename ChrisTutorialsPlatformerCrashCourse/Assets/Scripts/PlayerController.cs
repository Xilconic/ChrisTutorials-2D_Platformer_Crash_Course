using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 _moveInput;
    Rigidbody2D _rb;

    [Tooltip("Determines the walk-speed of the player.")]
    public float WalkSpeed = 5f;

    public bool IsMoving { get; private set; }
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(WalkSpeed > 0f, "'walkSpeed' must be greater than 0!");
        _rb = GetComponent<Rigidbody2D>();
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
}
