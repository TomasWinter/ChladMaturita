using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerHealth : EnemyHealth
{
    [SerializeField] HelmetHealth helmet;
    bool isHelmet = true;

    private void Start()
    {
        helmet.dieEvent?.AddListener(() => isHelmet = false);
    }
    public override void TakeDamage(int damage, object sender = null)
    {
        if (isHelmet)
            health -= damage/10;
        else
            health -= damage;

        hurtEvent?.Invoke();
        if (health <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }
}
