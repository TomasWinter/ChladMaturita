using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private void Start()
    {
        GlobalEvents.Instance.alarmRaised?.AddListener(OnAlarmRaised);
    }

    private void OnAlarmRaised()
    {
        MissionOverScript.Instance.Show(false,"Don't raise the alarm");
    }

    public void RaiseAlarm()
    {
        GlobalEvents.Instance.alarmRaised?.RemoveListener(OnAlarmRaised);
        GlobalEvents.Instance.alarmRaised.Invoke();
    }
}
