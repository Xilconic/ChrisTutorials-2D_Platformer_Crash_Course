using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Damagable))]
public class FlyingEye : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _animator;
    Damagable _damageable;

    Transform _nextWaypoint;
    int _waypointNumber = 0;

    [Tooltip("The script managing the detection for triggering bite attacks.")]
    public DetectionZone BiteDetectionZone;

    [Tooltip("The list of waypoints the Flying eye traverses.")]
    public List<Transform> Waypoints;

    [Tooltip("Determines the flight speed.")]
    public float FlightSpeed = 2f;

    [Tooltip("Determines the tolerance to being at a waypoint.")]
    public float WaypointDistance = 0.1f;

    [Tooltip("The collider to be enabled on death.")]
    public Collider2D DeathCollider;

    private bool _hasTarget = false;
    public bool HasTarget // TODO: Copied from Knight
    {
        get => _hasTarget;
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.HasTarget, value);
        }
    }

    public bool CanMove => _animator.GetBool(AnimationStrings.CanMove); // TODO: Copied from Knight

    void Awake()
    {
        Debug.Assert(Waypoints != null, "'Waypoints' must be set!");
        Debug.Assert(Waypoints.Count > 0, "'Waypoints' must contain at least 1 element!");
        Debug.Assert(BiteDetectionZone != null, "'BiteDetectionZone' must be set!");
        Debug.Assert(FlightSpeed > 0, "'FlightSpeed' must be greater than 0!");
        Debug.Assert(DeathCollider != null, "'DeathCollider' must be set!");
        Debug.Assert(DeathCollider.enabled == false, "'DeathCollider' must be disabled on awake!");

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damagable>();
    }

    private void Start()
    {
        _nextWaypoint = Waypoints[_waypointNumber];
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Code smell 'Law of Demeter violation': Given this usecase, AttackZone probably should expose a 'HasTarget' property instead.
        HasTarget = BiteDetectionZone.DetectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (_damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                _rb.velocity = Vector3.zero;
            }
        }
        else
        {
            // Flying Eye's by default have a gravityScale of 0 to prevent them from falling.
            // When dead, they cannot flap their wings, so gravity can do it's thing ;)
            _rb.gravityScale = 2f;
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            DeathCollider.enabled = true;
        }
    }

    private void OnEnable()
    {
        _damageable.Died.AddListener(OnDeath);
    }

    private void OnDisable()
    {
        _damageable.Died.RemoveListener(OnDeath);
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (_nextWaypoint.position - transform.position).normalized;

        float distanceToWaypoint = Vector2.Distance(_nextWaypoint.position, transform.position);

        _rb.velocity = directionToWaypoint * FlightSpeed;
        UpdateDirection();

        if(distanceToWaypoint <= WaypointDistance)
        {
            _waypointNumber++;
            if(_waypointNumber >= Waypoints.Count)
            {
                _waypointNumber = 0;
            }
            _nextWaypoint = Waypoints[_waypointNumber];
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        // If Flying Eye is facing to the right:
        if (localScale.x > 0)
        {
            if(_rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            if (_rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void OnDeath()
    {
        // Flying Eye's by default have a gravityScale of 0 to prevent them from falling.
        // When dead, they cannot flap their wings, so gravity can do it's thing ;)
        _rb.gravityScale = 2f;
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        DeathCollider.enabled = true;
    }
}
