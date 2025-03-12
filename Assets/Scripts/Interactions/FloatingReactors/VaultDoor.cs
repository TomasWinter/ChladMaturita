using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VaultDoor : MonoBehaviour
{
    [Header("vault")]
    [SerializeField] float openTime = 3;
    [SerializeField] float angle = 90;

    [SerializeField] Transform wheel;

    [Header("timelock")]
    [SerializeField] bool hasTimelock = false;
    [SerializeField] int timelockLoud = 60;
    [SerializeField] int timelockStealth = 30;
    [SerializeField] TextMeshPro screen;

    public UnityEvent OpenedEvent;
    public UnityEvent TimelockDoneEvent;

    public void Open()
    {
        StartCoroutine(Animate());
    }

    public void StartTimelock()
    {
        StartCoroutine(TimelockCountdown());
    }

    private IEnumerator TimelockCountdown()
    {
        for (int i = StateManager.Instance?.State == WaveState.Stealth ? timelockStealth : timelockLoud; i > 0;i--)
        {
            screen.text = $"{i/60}:{i%60:00}";
            yield return new WaitForSeconds(1);
        }
        screen.text = $"0:00";
        TimelockDoneEvent?.Invoke();
    }

    private IEnumerator Animate()
    {
        float original = Mathf.Repeat(transform.localRotation.eulerAngles.x + 90, 180) - 90;
        for (float i = 0; i < 1; i += 1/(100*openTime))
        {
            yield return new WaitForSeconds(0.01f);
            transform.localRotation = Quaternion.Euler(Anim.ElasticOut(original, angle, i, 0.5f), 0, 0);

        }
        transform.localRotation = Quaternion.Euler(angle, 0, 0);
        OpenedEvent?.Invoke();
    }
}
