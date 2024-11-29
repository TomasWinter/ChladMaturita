using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GenericHealthScript : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 1;
    protected int health;
    public UnityEvent dieEvent = new();
    public UnityEvent hurtEvent = new();

    protected void Awake()
    {
        health = maxHealth;
    }
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        hurtEvent?.Invoke();
        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        dieEvent?.Invoke();
        Destroy(gameObject);
    }
}
