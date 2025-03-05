using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] Material lightOn;
    [SerializeField] Material lightOff;

    Light[] lights;
    MeshRenderer[] renderers;

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        lights = GetComponentsInChildren<Light>();
    }

    public void TurnOn()
    {
        Switch(true);
    }

    public void TurnOff()
    {
        Switch(false);
    }

    private void Switch(bool b)
    {
        foreach (Light light in lights)
        {
            light.enabled = b;
        }
        foreach (MeshRenderer renderer in renderers)
        {
            Material[] mats = renderer.materials;
            mats[mats.Length - 1] = b ? lightOn : lightOff;
            renderer.materials = mats;
        }
    }
}
