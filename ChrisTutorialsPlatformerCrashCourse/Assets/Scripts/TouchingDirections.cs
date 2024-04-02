using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uses the collider to check directions to see if the object is currently on
/// the ground, touching a wall or touching a ceiling.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D _rb;
    CapsuleCollider2D _touchingCollider;
    RaycastHit2D[] _groundHits = new RaycastHit2D[5];
    [SerializeField]
    bool _isGrounded;
    Animator _animator;

    [Tooltip("??")]
    public ContactFilter2D CastFilter;

    [Tooltip("??")]
    public float groundDistance = 0.05f;
    
    public bool IsGrounded
    {
        get => _isGrounded;
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimationStrings.IsGrounded, value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchingCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = _touchingCollider.Cast(Vector2.down, CastFilter, _groundHits, groundDistance) > 0;
    }
}
