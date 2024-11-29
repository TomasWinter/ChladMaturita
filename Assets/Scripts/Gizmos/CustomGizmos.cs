using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class CustomGizmos : MonoBehaviour
{
    [SerializeField] bool show = true;

    private void OnDrawGizmos()
    {
        if (show)
        {
            DrawGizmo();
        }
    }

    protected abstract void DrawGizmo();
}