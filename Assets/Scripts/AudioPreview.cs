using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(AudioSource))]
public class AudioPreview : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AudioSource audioSource = (AudioSource)target;

        if (GUILayout.Button("Play"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
#endif