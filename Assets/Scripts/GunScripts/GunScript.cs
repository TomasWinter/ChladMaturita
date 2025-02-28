using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunScript : GunScriptParent
{
    protected override void PrivShoot()
    {
        base.PrivShoot();
        SpawnBullet();
    }
}
