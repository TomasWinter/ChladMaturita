using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkObjective : ObjectiveParent
{
    Collider collider;
    CollisionDetector collisionDetector;
    GameObject target;
    public WalkObjective(string t, string d, Collider c, WaypointInfo w = null, GameObject tar = null) : base(t, d, w)
    {
        collider = c;
        if (tar == null)
            target = PlayerHealth.Instance.gameObject;
        else
            target = tar;
    }

    public override void Initiate()
    {
        collisionDetector = collider.gameObject.AddComponent<CollisionDetector>();
        collisionDetector.Detected?.AddListener(ObjectiveDone);
        collisionDetector.Target = target;
        base.Initiate();
    }

    protected override void ObjectiveDone()
    {
        if (collisionDetector != null)
            MonoBehaviour.Destroy(collisionDetector);
        base.ObjectiveDone();
    }
}
