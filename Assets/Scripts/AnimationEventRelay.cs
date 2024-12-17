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

    public void PlayAudio(AudioClip clip)
    {
        AudioManager.Play(gameObject, clip, 10f);
    }
}
