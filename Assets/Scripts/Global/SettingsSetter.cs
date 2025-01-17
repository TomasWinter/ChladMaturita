using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsSetter : MonoBehaviour
{
    Settings Sets;
    private void Awake()
    {
        Sets = new Settings();
    }

    private void OnDestroy()
    {
        string json = JsonUtility.ToJson(Sets);
        File.WriteAllText("tothis.json", json);
    }
}
