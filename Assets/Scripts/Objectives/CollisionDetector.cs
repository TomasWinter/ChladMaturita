using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    public GameObject Target;

    public UnityEvent Detected = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Target)
            Detected?.Invoke();
    }
}
