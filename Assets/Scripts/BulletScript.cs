using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float timeToDie = 10;
    [SerializeField] int damage = 1;

    [SerializeField] OwnerType owner;

    public Color? color { get; private set; } = null;
    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDie)
            Destroy(gameObject);
    }

    public void Constructor(int d, OwnerType o, float? ttd = null, Color? c = null)
    {
        timeToDie = ttd ?? timeToDie;
        damage = d;
        owner = o;

        color = c ?? new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
        GetComponent<SpriteRenderer>().color = (Color)color;
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthScriptParent healthScript = other.GetComponent<HealthScriptParent>();
        if (healthScript != null)
        {
            healthScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shield" && owner != OwnerType.Player)
        {
            return;
        }
        else
        {
            HealthScriptParent healthScript = collision.transform.GetComponent<HealthScriptParent>();
            if (healthScript != null)
                healthScript.TakeDamage(damage);

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