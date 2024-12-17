using UnityEngine;
using UnityEngine.Events;


public abstract class ObjectiveParent
{
    protected bool enabled = false;
    protected bool done = false;

    protected WaypointInfo waypointInfo = null;

    public ObjectiveParent(string t,string d,WaypointInfo w = null)
    {
        Text = t;
        Description = d;
        waypointInfo = w;
    }

    public string Text { get; protected set; }
    public string Description { get; protected set; }

    UnityEvent Finished = new();
    public virtual void Initiate(UnityAction ua) 
    {
        enabled = true;
        Finished?.AddListener(ua);
        ShowWaypoint();
        IsFinished();
    }

    protected virtual void IsFinished()
    {
        if (done)
        {
            if (waypointInfo != null && waypointInfo.Instance != null)
            {
                BillboardManager.DestroyBillboard(waypointInfo.Instance);
            }

            enabled = false;
            Finished?.Invoke();
        }
    }

    protected virtual void ShowWaypoint()
    {
        if (waypointInfo != null && waypointInfo.Parent != null)
        {
            Transform t = null;
            if (waypointInfo.Sticky)
                t = waypointInfo.Parent;
            waypointInfo.Instance = BillboardManager.ImageBillboard(GlobalVals.WaypointTypeDictionary[waypointInfo.Type], waypointInfo.WaypointColor, waypointInfo.Parent.position, t);
        }
    }

    protected virtual void ObjectiveDone() 
    {
        done = true;
        if (!enabled)
            return;
        else
        {
            BillboardManager.DestroyBillboard(waypointInfo?.Instance);

            enabled = false;
            Finished?.Invoke();
            Finished = null;
        }
    }
}
