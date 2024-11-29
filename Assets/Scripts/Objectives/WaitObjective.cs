using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

public class WaitObjective : ObjectiveParent
{
    protected int time = 0;
    public WaitObjective(string t, string d, int ti) : base(t, d, null)
    {
        time = ti;
    }

    public override void Initiate(UnityAction ua)
    {
        Countdown();
        base.Initiate(ua);
    }

    protected async void Countdown()
    {
        await Task.Delay(time * 1000);
        if (enabled)
            base.ObjectiveDone();
    }
}
