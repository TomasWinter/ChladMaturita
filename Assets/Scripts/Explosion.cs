using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Explosion
{
    public static void Explode(Vector3 pos,int count,OwnerType owner,float force,Color? color = null)
    {
        color = color ?? new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
        if (GlobalVals.Instance?.Bullet != null)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnBullet(pos,force,owner,color);
            }
        }
    }

    private static void SpawnBullet(Vector3 pos,float force,OwnerType owner,Color? color)
    {
        GameObject bullet = MonoBehaviour.Instantiate(GlobalVals.Instance?.Bullet,pos,Random.rotation,GlobalVals.Instance?.BulletParent.transform);
        bullet.GetComponent<BulletScript>().Constructor(1, owner, null, color);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bullet.transform.forward * force;
    }
}
