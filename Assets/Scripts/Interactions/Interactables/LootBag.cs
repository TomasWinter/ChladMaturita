using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootBag : Interactable
{
    BagInfo Info = BagInfo.GetInfo(LootType.Air);
    public override void Interacted(bool b = true)
    {
        if (BagManager.Instance.SetBag(Info))
        {
            base.Interacted(b);
            StartCoroutine(die());
        }
    }

    private IEnumerator die()
    {
        yield return null;
        Destroy(gameObject);
    }

    public void SetInfo(BagInfo bagInfo)
    {
        Info = bagInfo;
        GetComponent<Rigidbody>().mass = Info.Weight;
        if (Info.LootType == LootType.Body)
            GetComponent<SpriteRenderer>().color = new Color(0.1792453f, 0.1792453f, 0.1792453f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SecureZone" && IsEnabled)
        {
            IsEnabled = false;
            LevelManager.Instance.SecureLoot(Info.Value);
            StartCoroutine(die());
        }
    }
}
