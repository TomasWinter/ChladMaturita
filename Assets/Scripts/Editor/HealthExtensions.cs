#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HealthScriptParent), true)]
public class HealthExtensions : Editor
{
    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlaying)
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("KILL"))
            {
                (target as HealthScriptParent).TakeDamage(int.MaxValue);
            }
        }
        else
        {
            base.OnInspectorGUI();
        }
    }
}
#endif