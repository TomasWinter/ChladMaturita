using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectHealth : HealthScriptParent
{
    public bool destroyOnDie = true;
    protected override void Die()
    {
        dieEvent?.Invoke();
        if (destroyOnDie)
            Destroy(gameObject);
        else
            Destroy(this);
    }
}
