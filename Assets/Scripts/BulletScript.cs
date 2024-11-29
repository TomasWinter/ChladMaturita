using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float timeToDie = 10;
    [SerializeField] int damage = 1;

    Color color = Color.white;
    float timer = 0;
    private void Start()
    {
        color = new Color(Random.Range(0.1f,1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
        GetComponent<SpriteRenderer>().color = color;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDie)
            Destroy(gameObject);
    }

    public void Constructor(int d, float? ttd = null)
    {
        timeToDie = ttd ?? timeToDie;
        damage = d;
    }

    private void OnTriggerEnter(Collider other)
    {
        GenericHealthScript healthScript = other.GetComponent<GenericHealthScript>();
        if (healthScript != null)
        {
            healthScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GenericHealthScript healthScript = collision.transform.GetComponent<GenericHealthScript>();
        if (healthScript != null)
            healthScript.TakeDamage(damage);
        Destroy(gameObject);
    }
}
