using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowWaypoints : MonoBehaviour, IDieOff
{
    [SerializeField] List<WaypointMarker> waypoints;
    WaypointMarker currentWaypoint = null;
    [SerializeField] Animator animator;
    int currentIndex = 0;

    NavMeshAgent agent;

    [SerializeField] float waitTime = 10;
    float timer = 0;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!enabled || waypoints.Count <= 0) return;

        if (currentWaypoint == null)
        {
            currentIndex = (int)Mathf.Repeat(currentIndex + 1, waypoints.Count);
            WalkTo(waypoints[currentIndex]);
            return;
        }

        if (agent.remainingDistance == 0)
        {
            animator.SetBool("Walking", false);
            agent.updateRotation = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, currentWaypoint.transform.rotation, 0.025f);
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                currentIndex = (int)Mathf.Repeat(currentIndex+1,waypoints.Count);
                WalkTo(waypoints[currentIndex], currentWaypoint);
                timer = 0;
            }
        }
        else
        {
            agent.updateRotation = true;
            animator.SetBool("Walking", true);
        }
    }

    private void WalkTo(WaypointMarker waypoint, WaypointMarker oldWaypoint = null)
    {
        if (waypoint.TakenBy == null)
        {
            waypoint.TakenBy = this;
            if (oldWaypoint != null && oldWaypoint.TakenBy == this) oldWaypoint.TakenBy = null;
            agent.SetDestination(waypoint.transform.position);
            currentWaypoint = waypoint;
        }
    }

    public void Shutdown()
    {
        animator.SetBool("Walking", false);
        agent.ResetPath();
        agent.updateRotation = true;
        this.enabled = false;
    }
}
