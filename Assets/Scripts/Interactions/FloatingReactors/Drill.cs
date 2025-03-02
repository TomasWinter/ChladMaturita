using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Drill : MonoBehaviour
{
    [SerializeField] int Time = 30;
    [SerializeField] TextMeshPro TextMeshPro;

    public UnityEvent DrillDoneEvent;

    SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.enabled = false;
        TextMeshPro.enabled = false;
    }

    public void Interacted()
    {
        TextMeshPro.enabled = true;
        SpriteRenderer.enabled = true;
        TextMeshPro.text = $"{Time}s";
        StartCoroutine(DrillIt());
    }

    IEnumerator DrillIt()
    {
        for (float i = 0;i < Time;i += 0.1f)
        {
            TextMeshPro.text = $"{(Time-i):F1}s";
            yield return new WaitForSeconds(0.1f);
        }
        DrillDoneEvent?.Invoke();
        SpriteRenderer.enabled = false;
        TextMeshPro.enabled = false;
    }
}
