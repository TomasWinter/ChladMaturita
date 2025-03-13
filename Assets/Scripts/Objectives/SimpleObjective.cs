using UnityEngine;
using UnityEngine.Events;


public class SimpleObjective : ObjectiveParent
{
    public SimpleObjective(string t,string d,UnityEvent ue, WaypointInfo w) : base(t,d,w)
    {
        ue.AddListener(ObjectiveDone);
    }
    public override void Initiate()
    {
        base.Initiate();
    }
    protected override void ObjectiveDone()
    {
        base.ObjectiveDone();
    }

}