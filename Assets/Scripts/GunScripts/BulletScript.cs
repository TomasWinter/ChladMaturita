using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] protected float timeToDie = 10;
    [SerializeField] protected int damage = 1;

    [SerializeField] protected OwnerType owner;

    public Color? color { get; private set; } = Color.white;
    protected float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDie)
            Destroy(gameObject);
    }

    public virtual void Constructor(int damage, OwnerType owner, float? ttd = null, Color? color = null)
    {
        timeToDie = ttd ?? timeToDie;
        this.damage = damage;
        this.owner = owner;

        this.color = color ?? new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
        GetComponent<SpriteRenderer>().color = (Color)this.color;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerShield" && owner != OwnerType.Player)
        {
            Destroy(gameObject);
        }
        else if (!(other.gameObject.tag == "Player" && owner == OwnerType.Player))
        {
            HealthScriptParent healthScript = other.GetComponent<HealthScriptParent>();
            if (healthScript != null)
            {
                healthScript.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.tag == "Shield" && owner != OwnerType.Player))
        {
            HealthScriptParent healthScript = collision.transform.GetComponent<HealthScriptParent>();
            if (healthScript != null)
                healthScript.TakeDamage(damage,collision);

            GameObject splat = GlobalVals.Instance.Splat;
            Vector3 normal = collision.contacts[0].normal;
            splat.transform.position = collision.contacts[0].point + (normal * 0.01f); ;
            splat.transform.rotation = Quaternion.LookRotation(normal);
            splat.GetComponent<SpriteRenderer>().color = (Color)color;
            splat.transform.parent = collision.transform;
            Destroy(gameObject);
        }
    }
}

public enum OwnerType
{
    Player,
    Enemy
}