using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingInteraction : Interactable
{
    [SerializeField] List<FloatingInteraction> group;
    [SerializeField] bool leader = false;

    private void Start()
    {
        if (leader)
        {
            group.Add(this);
            foreach (FloatingInteraction fi in group)
            {
                if (fi != this)
                    fi.SetList(group);
            }
            Activate(IsEnabled);
        }
    }
    public override void Interacted(bool b = true)
    {
        foreach (FloatingInteraction fi in group)
        {
            fi.SetActive(false);
        }
        SetActive(false);
        base.Interacted(b);
    }

    public override void SetPointing(bool b)
    {
        if (IsEnabled)
        {
            float transparency = b ? 1 : 0.3f;
            transform.localScale = b ? new Vector3(1.1f, 1.1f, 1f) : Vector3.one;
            foreach (SpriteRenderer sr in transform.parent.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, transparency);
            }
        }
    }

    private void SetActive(bool b)
    {
        interacting = false;
        IsEnabled = b;
        float transparency = b ? 0.3f : 0;
        transform.localScale = Vector3.one;

        foreach (SpriteRenderer sr in transform.parent.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, transparency);
        }
    }

    private void SetList(List<FloatingInteraction> list)
    {
        group = list;
    }

    public override void Activate(bool b = true)
    {
        if (leader)
        {
            foreach (FloatingInteraction fi in group)
            {
                if (fi != this)
                    fi.Activate(b);
            }
        }
        SetActive(b);
        base.Activate(b);
    }
}