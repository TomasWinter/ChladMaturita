using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealthScript
{
    [SerializeField] bool spawnPager;
    protected override void Die()
    {
        GlobalEvents.Instance.enemyEliminated?.Invoke();
        foreach (EnemyBehavior eb in GetComponents<EnemyBehavior>())
        {
            eb.enabled = false;
        }
        if (StateManager.State == WaveState.Stealth)
        {
            Instantiate(GlobalVals.Instance.Grave, transform.position, Quaternion.identity);
        }

        base.Die();
    }
}
