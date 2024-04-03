using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{
    Collider2D _attackCollider;

    [Tooltip("The amount of damage this attack does")]
    public int AttackDamage = 10;

    [Tooltip("The knockback of the attack. By default no knockback. Positive value knock away, negative values pull in.")]
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
            var deliveredKnockback = transform.parent.localScale.x > 0 ?
                KnockBack :
                new Vector2(-KnockBack.x, KnockBack.y);
            bool gotHit = damagable.Hit(AttackDamage, deliveredKnockback);
            if(gotHit)
            {
                Debug.Log($"{collision.name} hit for {AttackDamage}");
            }
        }
    }
}
