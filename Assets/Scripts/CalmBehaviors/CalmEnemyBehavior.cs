using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CalmEnemyBehavior : CalmBehavior
{
    private void Awake()
    {
        DetectionLvl = 2;
    }

    protected override IEnumerator DetectedSomething()
    {
        transform.GetComponent<FollowWaypoints>().Shutdown();
        yield return new WaitForSeconds(1);
        GetComponent<EnemyBehavior>().enabled = true;
        alertText.text = "ì";
        yield return new WaitForSeconds(3);
        GlobalEvents.Instance.alarmRaised?.Invoke();
    }

    public override void AlarmRaised()
    {
        transform.GetComponent<FollowWaypoints>().Shutdown();
        GetComponent<EnemyBehavior>().enabled = true;
        Shutdown();
    }
}
