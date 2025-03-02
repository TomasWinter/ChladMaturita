using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterDoor : MonoBehaviour
{
    float original;
    [SerializeField] float desired = 2.5f;

    private void Start()
    {
        original = transform.localPosition.y;
    }

    public void Open()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localPosition = new Vector3(Anim.ElasticEaseOut(original, desired, i, 0.5f), 0,0);

        }
        transform.localPosition = new Vector3(desired,0,0);
    }
}
