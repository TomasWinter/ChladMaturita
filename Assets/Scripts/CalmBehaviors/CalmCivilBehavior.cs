using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CalmCivilBehavior : CalmBehavior
{
    NavMeshAgent agent;
    [SerializeField] Animator animator;
    private void Awake()
    {
        DetectionLvl = 3;
        agent = GetComponent<NavMeshAgent>();
    }

    protected override IEnumerator DetectedSomething()
    {
        transform.GetComponent<FollowWaypoints>()?.Shutdown();
        yield return new WaitForSeconds(1);
        alertText.text = "ì";
        yield return new WaitForSeconds(3);
        GlobalEvents.Instance.alarmRaised?.Invoke();
    }

    public override void AlarmRaised()
    {
        Flee();
        Shutdown();
    }

    private void Flee()
    {
        transform.GetComponent<FollowWaypoints>()?.Shutdown();
        agent.updateRotation = true;
        animator.SetBool("Walking", true);
        agent.SetDestination(ExitMarker.FindClosest(transform.position).transform.position);
        agent.speed *= 2;
    }
}
