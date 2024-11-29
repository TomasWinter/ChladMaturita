using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardingScript : MonoBehaviour
{
    Transform followTransform;
    [SerializeField] Vector3 followAxis = Vector3.one;
    [SerializeField] bool scale = false;
    [SerializeField] float bigger = 1;

    private void Start()
    {
        followTransform = PlayerHealth.Instance.transform;
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.Scale(transform.position - followTransform.position, followAxis), Vector3.up);
        if (scale)
        {
            transform.localScale = (new Vector3(1,1,0) * (transform.position - followTransform.position).magnitude / 300 + new Vector3(0, 0, 0.1f)) * bigger;
        }
    }
}
