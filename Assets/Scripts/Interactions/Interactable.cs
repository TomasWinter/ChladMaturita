using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected float interactionTime = 0;
    [SerializeField] protected bool defaultEnabled = true;
    [SerializeField] protected string interactionText = "interact";
    protected bool interacting = false;

    public UnityEvent InteractedEvent;

    public bool IsEnabled { get; protected set; }
    public float InteractionTime { get { return interactionTime; } }
    public string InteractionText { get { return interactionText; } }

    protected void Awake()
    {
        IsEnabled = defaultEnabled;
    }

    public virtual void SetPointing(bool b) { }
    public virtual void IsInteracting(bool b)
    {
        interacting = b;
    }
    public virtual void Interacted(bool b = true)
    {
        interacting = false;
        IsEnabled = false;
        InteractedEvent?.Invoke();
    }
    public virtual void Activate()
    {
        IsEnabled = true;
    }
}