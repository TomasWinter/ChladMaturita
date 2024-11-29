using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmoStuff
{
    public static readonly Dictionary<GizmoType, string> GizmoDictionary = new Dictionary<GizmoType, string>()
    {
        { GizmoType.Waypoint, "Waypoint" },
        { GizmoType.Exit, "Exit" },
        { GizmoType.Enemy, "AngryFace" }
    };
}

public enum GizmoType
{
    Waypoint,
    Exit,
    Enemy
}