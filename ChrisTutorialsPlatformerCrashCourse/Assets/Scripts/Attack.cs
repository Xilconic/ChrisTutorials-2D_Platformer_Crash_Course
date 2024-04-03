using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{
    Collider2D _attackCollider;

    [Tooltip("The amount of damage this attack does")]
    public int AttackDamage = 10;

    [Tooltip("The knockback of the attack. By default no knockback.")]
    public Vector2 KnockBack = Vector2.zero;

    private void Awake()
    {
        _attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            bool gotHit = damagable.Hit(AttackDamage, KnockBack);
            if(gotHit)
            {
                Debug.Log($"{collision.name} hit for {AttackDamage}");
            }
        }
    }
}
