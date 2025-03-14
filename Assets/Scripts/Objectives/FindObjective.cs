using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class FindObjective : ObjectiveParent
{
    protected int time = 0;
    public FindObjective(string t, string d, UnityEvent ue,int ti, WaypointInfo w) : base(t, d, w)
    {
        waypointInfo = w;
        time = ti;
        ue.AddListener(ObjectiveDone);
    }
    public override void Initiate()
    {
        Countdown();
        base.Initiate();
    }

    protected override void ShowWaypoint()
    {
        return;
    }

    protected async void Countdown()
    {
        await Task.Delay(time * 1000);
        if (enabled)
            base.ShowWaypoint();
    }
}
