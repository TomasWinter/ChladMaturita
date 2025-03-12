using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : BulletScript
{
    [SerializeField] int shrapnelNumber;

    public override void Constructor(int shrapnelNum, OwnerType owner, float? ttd = null, Color? color = null)
    {
        shrapnelNumber = shrapnelNum;
        base.Constructor(0, owner, ttd, color);
    }
    override protected void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.tag == "Shield" && owner != OwnerType.Player))
        {
            Explode();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthScriptParent>() != null)
            Explode();
    }

    private void Explode()
    {
        GetComponent<Collider>().enabled = false;
        Explosion.Explode(transform.position, shrapnelNumber, owner, 10,color);
        Destroy(gameObject);
    }
}
