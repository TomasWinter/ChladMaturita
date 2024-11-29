using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventRelay : MonoBehaviour
{
    public void RelayDone(string name = "")
    {
        foreach (IAnimationReactor a in transform.GetComponentsInChildren<IAnimationReactor>())
        {
            a.AnimDone(name);
        }
    }

    public void RelayStart(string name = "")
    {
        foreach (IAnimationReactor a in transform.GetComponentsInChildren<IAnimationReactor>())
        {
            a.AnimStart(name);
        }
    }
}
