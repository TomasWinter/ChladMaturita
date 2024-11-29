using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CalmEnemyBehavior : CalmBehavior
{
    private void Awake()
    {
        DetectionLvl = 1;
    }

    protected override IEnumerator DetectedSomething()
    {
        transform.GetComponent<FollowWaypoints>().Shutdown();
        yield return new WaitForSeconds(1);
        GetComponent<EnemyBehavior>().enabled = true;
        alertText.text = "�";
        yield return new WaitForSeconds(3);
        GlobalEvents.Instance.alarmRaised?.Invoke();
        Shutdown();
    }

    public override void AlarmRaised()
    {
        GetComponent<EnemyBehavior>().enabled = true;
        Shutdown();
    }
}
