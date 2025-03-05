using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlassHealth : ObjectHealth
{
    [SerializeField] float slingForce = 2;
    [SerializeField] float dieTime = 10;
    [SerializeField] int segments = 4;

    [SerializeField] AudioClip glassShattering;

    Mesh mesh;
    Material[] materials;

    Vector3 normal;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        materials = GetComponent<MeshRenderer>().materials;
    }

    protected override void Die()
    {
        AudioManager.Play(transform.position, glassShattering, 20, 0.8f, 1f);
        GetComponent<Collider>().enabled = false;
        Vector3 scale = new Vector3(Mathf.Abs(transform.lossyScale.x), Mathf.Abs(transform.lossyScale.y) / segments, Mathf.Abs(transform.lossyScale.z) / segments);
        for (int z = -segments/2; z < segments/2;z++)
        {
            float posZ = scale.z * z + scale.z / 2;
            for (int y = -segments / 2; y < segments / 2; y++)
            {
                float posY = scale.y * y + scale.y / 2;
                GameObject shard = CreateShard(scale);
                shard.transform.position = transform.rotation * new Vector3(0, posY, posZ) + transform.position;
                if (normal != null)
                    shard.GetComponent<Rigidbody>().AddRelativeForce(-normal * slingForce,ForceMode.VelocityChange);
            }
        }
        base.Die();
    }

    public override void TakeDamage(int damage, object sender = null)
    {
        if (sender != null && sender.GetType() == typeof(Collision))
        {
            Collision collision = (Collision)sender;
            normal = collision.contacts.First().normal;
        }
        base.TakeDamage(damage, sender);
    }

    private GameObject CreateShard(Vector3 scale)
    {
        GameObject shard = new GameObject("GlassShard");

        shard.AddComponent<MeshRenderer>().materials = materials;
        shard.AddComponent<MeshFilter>().mesh = mesh;
        shard.AddComponent<BoxCollider>().excludeLayers = LayerMask.GetMask("Humanoids");
        shard.AddComponent<Rigidbody>();
        shard.AddComponent<DeathTimer>().dieTime = dieTime;

        shard.transform.SetParent(transform.parent);
        shard.transform.position = transform.position;
        shard.transform.rotation = transform.rotation;
        shard.transform.localScale = scale;

        return shard;
    }
}
