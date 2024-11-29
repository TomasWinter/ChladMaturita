using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExitMarker : MonoBehaviour
{
    public static List<ExitMarker> Instances { get; private set; } = new List<ExitMarker>();

    public static ExitMarker FindClosest(Vector3 pos)
    {
        ExitMarker closest = null;
        float distance = float.MaxValue;
        foreach (ExitMarker EM in Instances)
        {
            if ((EM.transform.position - pos).magnitude < distance)
            {
                closest = EM;
                distance = (EM.transform.position - pos).magnitude;
            }
        }
        return closest;
    }

    private void Awake()
    {
        Instances.Add(this);
    }

    private void OnDestroy()
    {
        Instances.Remove(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Civilian")
        {
            foreach (IDieOff IDO in other.gameObject.GetComponents<IDieOff>())
                IDO.Shutdown();
            Destroy(other.gameObject);
        }
    }
}
