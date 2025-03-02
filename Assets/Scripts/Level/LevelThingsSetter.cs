using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThingsSetter : MonoBehaviour
{
    [SerializeField] State defaultState;
    [SerializeField] float delay;

    private void Start()
    {
        if (defaultState == State.Loud)
            StartCoroutine(BeginAssault());
    }

    private IEnumerator BeginAssault()
    {
        yield return new WaitForSeconds(delay);
        GlobalEvents.Instance?.alarmRaised?.Invoke();
    }

    private enum State
    {
        Stealth,
        Loud
    }
}
