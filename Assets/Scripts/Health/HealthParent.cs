using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class HealthScriptParent : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 1;
    protected float health;
    public UnityEvent dieEvent = new();
    public UnityEvent hurtEvent = new();

    protected bool dead = false;

    protected void Awake()
    {
        health = maxHealth;
    }
    public virtual void TakeDamage(int damage,object sender = null)
    {
        health -= damage;
        hurtEvent?.Invoke();
        if (health <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        dieEvent?.Invoke();
        Destroy(gameObject);
    }
}
