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
                Object.Destroy(waypointInfo.Instance);
            }

            enabled = false;
            Finished?.Invoke();
        }
    }

    protected virtual void ShowWaypoint()
    {
        if (waypointInfo != null && waypointInfo.Parent != null)
        {
            GameObject w = Object.Instantiate(GlobalVals.Instance.WaypointPrefab);
            SpriteRenderer SR = w.GetComponent<SpriteRenderer>();
            SR.sprite = GlobalVals.WaypointTypeDictionary[waypointInfo.Type];
            SR.color = waypointInfo.WaypointColor;

            w.transform.position = waypointInfo.Parent.position;
            if (waypointInfo.Sticky)
            {
                w.transform.parent = waypointInfo.Parent;
            }

            waypointInfo.Instance = w;
        }
    }

    protected virtual void ObjectiveDone() 
    {
        done = true;
        if (!enabled)
            return;
        else
        {
            if (waypointInfo != null && waypointInfo.Instance != null)
            {
                Object.Destroy(waypointInfo.Instance);
            }

            enabled = false;
            Finished?.Invoke();
            Finished = null;
        }
    }
}
