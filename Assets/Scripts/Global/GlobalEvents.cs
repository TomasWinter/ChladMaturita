using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents Instance {  get; private set; }

    public UnityEvent enemyEliminated;
    public UnityEvent alarmRaised;
    public UnityEvent lootPickedUp;

    private void Awake()
    {
        Instance = this;
    }
}
