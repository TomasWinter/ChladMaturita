using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : Interactable
{
    int uses = 4;
    public override void Interacted(bool b = true)
    {
        PlayerHealth.Instance.Heal();
        if (--uses <= 0)
        {
            StartCoroutine(die());
        }
        else
        {
            Destroy(transform.GetChild(0)?.gameObject);
        }
    }

    private IEnumerator die()
    {
        yield return null;
        Destroy(gameObject);
    }
}
