using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : BasicEnemyBehavior
{
    [SerializeField] GameObject shield;
    private void Awake()
    {
        shield = Instantiate(shield,transform.position,transform.rotation);
    }
    protected override void Update()
    {
        base.Update();
        shield.transform.position = transform.position;
        shield.transform.rotation = transform.rotation;
    }

    protected override void LookAtTarget(Vector3 targetDir)
    {
        Quaternion x = Quaternion.LookRotation(targetDir, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, x, 0.005f);
    }

    private void OnDestroy()
    {
        Destroy(shield);
    }
}
