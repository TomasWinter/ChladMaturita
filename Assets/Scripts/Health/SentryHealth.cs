using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryHealth : ObjectHealth
{
    [SerializeField] AudioClip sound;
    [SerializeField] ParticleSystem explosion;
    bool disappearing = false;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position+Vector3.down*0.6f);
        if (Physics.Raycast(transform.position, Vector3.down, 0.6f, GlobalVals.Instance.RaycastLayermask))
        {
            rb.drag = 5;
        }
        else
        {
            rb.drag = 0;
        }
    }

    protected override void Die()
    {
        Sentry s = GetComponent<Sentry>();
        if (s != null )
            s.Shutdown();
        Explode();
    }

    public void Explode()
    {
        health = 0;
        dead = true;

        
        rb.freezeRotation = false;
        rb.drag = 0;

        Vector3 rndVector = (Vector3.up + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)))*10;
        rb.AddRelativeForce(rndVector, ForceMode.Impulse);
        rb.AddRelativeTorque(Vector3.up + Vector3.right, ForceMode.Impulse);

        AudioManager.Play(gameObject, sound, 25);
        explosion.Emit(1);

        if (!disappearing)
            StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        disappearing = true;
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
