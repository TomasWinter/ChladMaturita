using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObjectiveTemplate
{
    public ObjectiveType type;
    [Space]
    public int amount = 0;
    public bool active = false;
    [Space]
    public string text = "Oops!Objective name 404";
    public string description = "Sup dood. Do the thing and u cool B)";
    [Space]
    public WaypointInfo waypointInfo = null;
    [Space]
    public Collider collider = null;
    [Header("Listen Event")]
    public string eventName;
    public MonoBehaviour mono;
    /*[Header("Activate Event")]
    public string activateEventName;
    public MonoBehaviour activateMono;*/
}