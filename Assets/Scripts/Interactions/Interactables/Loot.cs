using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
    [SerializeField] protected LootType type = LootType.Air;
    protected BagInfo Info;

    protected virtual void Start()
    {
        Info = BagInfo.GetInfo(type);
    }
    public override void Interacted(bool b = true)
    {
        if (BagManager.Instance.SetBag(Info))
        {
            base.Interacted(b);
            StartCoroutine(die());
        }
    }

    protected IEnumerator die()
    {
        yield return null;
        Destroy(gameObject);
    }

    /*public bool SetInfo(BagInfo bagInfo)
    {
        if (Info == null)
        {
            Info = bagInfo;
            GetComponent<Rigidbody>().mass = Info.Weight;
            return true;
        }
        return false;
    }*/
}
