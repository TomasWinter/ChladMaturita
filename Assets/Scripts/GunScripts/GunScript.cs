using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunScript : GunScriptParent
{
    protected override void PrivShoot()
    {
        base.PrivShoot();

        GameObject bullet = Instantiate(GlobalVals.Instance.Bullet, bulletSpawn.position, bulletSpawn.rotation * Quaternion.Euler(0, -90, 0), GlobalVals.Instance.BulletParent.transform);
        bullet.GetComponent<BulletScript>().Constructor(scriptableObject.Damage,owner);

        Rigidbody brb = bullet.GetComponent<Rigidbody>();
        Vector3 rndVector = new Vector3(Random.Range(-scriptableObject.Spread, scriptableObject.Spread), Random.Range(-scriptableObject.Spread, scriptableObject.Spread), 1);
        brb.AddRelativeForce(rndVector * scriptableObject.BulletForce, ForceMode.VelocityChange);
    }
}
