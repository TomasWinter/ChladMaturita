using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehavior : MonoBehaviour
{
    [Header("Range")]
    [SerializeField] protected float minDistance = 1;
    [SerializeField] protected float standDistance = 5;
    [SerializeField] protected float maxDistance = 10;
    [SerializeField] protected float tolerance = 1;
    [Header("Walking")]
    [SerializeField] protected float walkSpeed = 1;
    [SerializeField] protected float runSpeed = 2;
    [Header("Attack")]
    [SerializeField] protected int damage = 1;

    protected Rigidbody rigidBody;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected GameObject target;
    virtual protected void Start()
    {
        target = PlayerHealth.Instance.gameObject;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        tolerance = Random.Range(tolerance-1f,tolerance+1f);
        agent.ResetPath();
    }

    private void OnEnable()
    {
        if (agent != null)
            agent.ResetPath();
    }

    protected void Update()
    {
        float distance = (gameObject.transform.position - target.transform.position).magnitude;
        Vector3 targetDir = new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
        if (Physics.Raycast(transform.position, targetDir, out RaycastHit hit, maxDistance))
        {
            if (distance < minDistance)
            {
                agent.ResetPath();
                transform.rotation = Quaternion.LookRotation(targetDir, transform.up);
                rigidBody.AddForce(-transform.forward * walkSpeed);
            }
            else if (distance < standDistance && hit.collider.gameObject == target)
            {
                agent.ResetPath();
                transform.rotation = Quaternion.LookRotation(targetDir, transform.up);
                Attack();
            }
            else if (distance < maxDistance && hit.collider.gameObject == target)
            {
                transform.rotation = Quaternion.LookRotation(targetDir, transform.up);
                Attack();
                agent.SetDestination(target.transform.position);
                agent.speed = walkSpeed;
            }
            else
            {
                agent.SetDestination(target.transform.position);
                agent.speed = runSpeed;
            }
        }
        else
        {
            agent.SetDestination(target.transform.position);
            agent.speed = runSpeed;
        }
    }

    abstract protected void Attack();
}
