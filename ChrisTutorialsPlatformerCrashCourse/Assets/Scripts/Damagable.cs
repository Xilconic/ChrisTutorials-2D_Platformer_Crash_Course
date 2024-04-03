using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Damagable : MonoBehaviour
{
    Animator _animator;

    private float _timeSinceHit = 0;

    [Tooltip("Determines how long, in seconds, this component cannot take any damage after taken a hit.")]
    public float InvincibilityTime = 0.25f; // In Seconds

    [SerializeField]
    int _maxHealth = 100;
    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    int _health = 100;
    public int Health
    {
        get => _health;
        set
        {
            _health = value;

            if(_health <= 0)
            { 
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
            _animator.SetBool(AnimationStrings.IsAlive, value);
        }
    }

    [SerializeField]
    bool _isInvincible = false;
    public bool IsInvincible
    {
        get => _isInvincible;
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(IsInvincible)
        {
            if (_timeSinceHit > InvincibilityTime)
            {
                _isInvincible = false;
                _timeSinceHit = 0;
            }

            _timeSinceHit += Time.deltaTime;
        }
    }

    public void Hit(int damage)
    {
        if(IsAlive && !IsInvincible)
        {
            Health -= damage;
            _isInvincible = true;
        }
    }
}
