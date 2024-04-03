using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{
    Collider2D _attackCollider;

    [Tooltip("The amount of damage this attack does")]
    public int AttackDamage = 10;

    private void Awake()
    {
        _attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            damagable.Hit(AttackDamage);
            Debug.Log($"{collision.name} hit for {AttackDamage}");
        }
    }
}
