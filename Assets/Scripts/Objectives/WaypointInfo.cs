using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaypointInfo
{
    public Transform Parent;
    public bool Sticky;
    public WaypointType Type;
    public Color WaypointColor = new Color(0.7647f, 0.6118f, 0.2118f,1f);

    [HideInInspector]public GameObject Instance;
}


public enum WaypointType
{
    Interact,
    Target,
    Walk,
    Arrow,
    Secure
}