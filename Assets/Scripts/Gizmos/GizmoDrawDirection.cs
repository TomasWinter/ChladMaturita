using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GizmoDrawDirection : CustomGizmos
{
    [SerializeField][Range(0,1)] float scale = 1;

    protected override void DrawGizmo()
    {
        Gizmos.DrawLine(transform.position + transform.right * scale * 0.5f,transform.position + transform.forward * scale);
        Gizmos.DrawLine(transform.position - transform.right * scale * 0.5f, transform.position + transform.forward * scale);
    }

}