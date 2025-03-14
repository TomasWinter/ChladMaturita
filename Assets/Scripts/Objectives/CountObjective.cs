using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountObjective : ObjectiveParent
{
    public int timeDone { get; protected set; } = 0;
    public int required { get; protected set; } = 0;
    public bool showCount { get; protected set; } = true;

    public CountObjective(string t, string d, UnityEvent ue, int r, bool b, WaypointInfo w) : base(t, d, w)
    {
        showCount = b;
        required = r;
        ue.AddListener(CountUp);
    }

    protected void CountUp()
    {
        timeDone++;
        if (showCount && enabled)
        {
            PlayerGuiManager.Instance.ChangeObjective(this,true);
        }
        if (timeDone >= required)
        {
            ObjectiveDone();
        }
        
    }
}
