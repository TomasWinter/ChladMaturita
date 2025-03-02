using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class FovExtensions
{
    public static Vector3 Deg2Vec(float deg,Vector3 forwardVector)
    {
        Vector3 finalVector = new Vector3(Mathf.Sin(deg * Mathf.Deg2Rad), 0, Mathf.Cos(deg * Mathf.Deg2Rad));
        Quaternion rotation = Quaternion.LookRotation(forwardVector);
        return rotation * finalVector;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(CalmBehavior), true)]
public class FovEditor : Editor
{
    static float RayDensity = 5f;
    static bool showGui = false;
    public override void OnInspectorGUI()
    {
        showGui = EditorGUILayout.Foldout(showGui, "FOV Preview");
        if (showGui)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            GUILayout.Label("Ray Density", GUILayout.Width(70));
            GUILayout.Space(10);
            RayDensity = Mathf.Clamp(EditorGUILayout.FloatField(RayDensity, GUILayout.Width(70)), 0f, 1000f);
            GUILayout.Space(10);
            RayDensity = GUILayout.HorizontalSlider(RayDensity, 0.1f, 10f, GUILayout.Width(200));

            GUILayout.EndHorizontal();
            GUILayout.Space(30);
        }
        base.OnInspectorGUI();

    }
    private void OnSceneGUI()
    {
        float radius = serializedObject.FindProperty("radius").floatValue;
        float angle = serializedObject.FindProperty("fov").floatValue;
        CalmBehavior calmBehavior = (CalmBehavior)target;
        Transform targetTransform = ((CalmBehavior)target).transform;

        Handles.color = Color.white;
        Handles.DrawWireDisc(targetTransform.position, new Vector3(0, 1, 0), radius);

        Handles.color = Color.yellow;
        Vector3 pos = targetTransform.position + new Vector3(0,0.5f,0);
        Handles.DrawLine(pos, pos + FovExtensions.Deg2Vec(angle,targetTransform.forward) * radius);
        Handles.DrawLine(pos, pos + FovExtensions.Deg2Vec(-angle, targetTransform.forward) * radius);
        //RenderRealFov(angle,targetTransform,radius);
    }

    private void RenderRealFov(float angle, Transform targetTransform, float radius)
    {
        Handles.color = new Color(0.85f,0,0);
        for (float i = -angle + 10 / RayDensity; i < angle; i = i+10/RayDensity)
        {
            if (Physics.Raycast(targetTransform.position, FovExtensions.Deg2Vec(i,targetTransform.forward), out RaycastHit hit, radius))
            {
                Handles.DrawLine(targetTransform.position, hit.point);
                //Handles.color = new Color(0.5f,0,0);
                //Handles.DrawLine(hit.point, targetTransform.position + FovExtensions.Deg2Vec(i, targetTransform.forward) * radius);
                //Handles.color = Color.red;
            }
            else
                Handles.DrawLine(targetTransform.position, targetTransform.position + FovExtensions.Deg2Vec(i, targetTransform.forward) * radius);
        }
    }
}
#endif