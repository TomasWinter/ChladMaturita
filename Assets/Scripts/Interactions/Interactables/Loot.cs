using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Loot : Interactable
{
    [SerializeField] protected LootType type = LootType.Air;
    protected BagInfo Info;

    public UnityEvent LootedEvent;
    protected virtual void Start()
    {
        Info = BagInfo.GetInfo(type);
    }
    public override void Interacted(bool b = true)
    {
        if (BagManager.Instance.SetBag(Info))
        {
            LootedEvent?.Invoke();
            base.Interacted(b);
            StartCoroutine(die());
        }
    }

    protected IEnumerator die()
    {
        yield return null;
        Destroy(gameObject);
    }

}
