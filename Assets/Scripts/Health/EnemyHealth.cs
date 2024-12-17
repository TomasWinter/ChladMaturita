using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthScriptParent
{
    [SerializeField] bool spawnPager;
    protected override void Die()
    {
        GlobalEvents.Instance.enemyEliminated?.Invoke();
        foreach (EnemyBehavior eb in GetComponents<EnemyBehavior>())
        {
            eb.enabled = false;
        }
        if (StateManager.Instance?.State == WaveState.Stealth)
        {
            GameObject go = Instantiate(GlobalVals.Instance.Grave, transform.position, Quaternion.identity);
            go.GetComponent<Grave>().SetPager(spawnPager);
        }

        base.Die();
    }
}
