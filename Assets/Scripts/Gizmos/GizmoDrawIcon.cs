using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GizmoDrawIcon : CustomGizmos
{
    [SerializeField] GizmoType type;
    [SerializeField] Color color = Color.white;
    [SerializeField] bool scale = true;

    protected override void DrawGizmo()
    {
        Gizmos.DrawIcon(transform.position, GizmoStuff.GizmoDictionary[type], scale, color);
    }
}