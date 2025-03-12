using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SecurityTruckDoor : MonoBehaviour
{
    [SerializeField][Range(70, 120)] float angle = 90;

    [SerializeField] Transform Door1;
    [SerializeField] Transform Door2;

    public void Open()
    {
        StartCoroutine(Animate(angle));
    }

    private IEnumerator Animate(float desired)
    {
        float original1 = Mathf.Repeat(Door1.localRotation.eulerAngles.y, 180);
        for (float i = 0; i < 1; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            Door1.localRotation = Quaternion.Euler(0, Anim.ElasticOut(original1, desired, i, 0.5f), 0);
            Door2.localRotation = Quaternion.Euler(0, -Door1.localRotation.eulerAngles.y, 0);
        }
        Door1.localRotation = Quaternion.Euler(0, desired, 0);
        Door2.localRotation = Quaternion.Euler(0, -desired, 0);
    }
}
