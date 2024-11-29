using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SusIndicator : MonoBehaviour
{
    public sbyte SusLvl
    {
        get
        {
            return susLvl.Max();
        }
        set
        {
            if (value < 0)
                susLvl.Remove((sbyte)-value);
            else if (value != 0)
                susLvl.Add(value);
        }
    }
    [SerializeField] List<sbyte> susLvl = new() { 0 };
    public bool MultiDetection = true;
    [HideInInspector]public CalmBehavior CurrentDetector = null;
}
