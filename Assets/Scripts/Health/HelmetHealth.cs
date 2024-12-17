using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HelmetHealth : HealthScriptParent
{
    Rigidbody rb;
    bool active = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected override void Die()
    {
        if (active)
        {
            active = false;
            dieEvent?.Invoke();
            rb.isKinematic = false;
            Vector3 rndVector = Vector3.up + new Vector3(Random.Range(-0.5f,0.5f),0, Random.Range(-0.5f, 0.5f));
            rb.AddRelativeForce(rndVector,ForceMode.Impulse);
            rb.AddRelativeTorque(Vector3.up+Vector3.right,ForceMode.Impulse);
            StartCoroutine(DieCollider());
        }
    }

    private IEnumerator DieCollider()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider>().enabled = false;
    }
}
