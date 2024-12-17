using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class BillboardManager
{
    public static GameObject ImageBillboard(Sprite sprt,Color col,Vector3 pos, Transform par = null)
    {
        GameObject w = Object.Instantiate(GlobalVals.Instance.WaypointPrefab);
        SpriteRenderer SR = w.GetComponent<SpriteRenderer>();
        SR.sprite = sprt;
        SR.color = col;

        w.transform.position = pos;
        if (par != null)
            w.transform.SetParent(par);

        return w;
    }

    public static GameObject TextBillboard(string strng, Color col, Vector3 pos, Transform par = null)
    {
        GameObject w = Object.Instantiate(GlobalVals.Instance.TextBillboardPrefab);
        TextMeshPro tmp = w.GetComponent<TextMeshPro>();
        tmp.text = strng;
        tmp.color = col;

        w.transform.position = pos;
        if (par != null)
            w.transform.SetParent(par);

        return w;
    }

    public static void DestroyBillboard(GameObject obj)
    {
        MonoBehaviour.Destroy(obj);
    }
}

/*
        GameObject w = Object.Instantiate(GlobalVals.Instance.WaypointPrefab);
        SpriteRenderer SR = w.GetComponent<SpriteRenderer>();
        SR.sprite = GlobalVals.WaypointTypeDictionary[waypointInfo.Type];
        SR.color = waypointInfo.WaypointColor;

        w.transform.position = waypointInfo.Parent.position;
        if (waypointInfo.Sticky)
        {
            w.transform.parent = waypointInfo.Parent;
        }
        return w;
*/