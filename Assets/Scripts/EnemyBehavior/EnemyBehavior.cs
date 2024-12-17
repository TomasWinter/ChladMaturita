using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehavior : MonoBehaviour
{
    public static List<EnemyBehavior> ActiveEnemies = new List<EnemyBehavior>();

    [Header("Range")]
    [SerializeField] protected float minDistance = 1;
    [SerializeField] protected float standDistance = 5;
    [SerializeField] protected float maxDistance = 10;
    [SerializeField] protected float tolerance = 1;
    [Header("Walking")]
    [SerializeField] protected float walkSpeed = 1;
    [SerializeField] protected float runSpeed = 2;
    [Header("WalkingAnimator")]
    [SerializeField] protected Animator walkAnimator;

    protected Rigidbody rigidBody;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected GameObject target;
    virtual protected void Start()
    {
        ActiveEnemies.RemoveAll(x => x == null);
        ActiveEnemies.Add(this);

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

    protected virtual void Update()
    {
        float distance = (gameObject.transform.position - target.transform.position).magnitude;
        Vector3 targetDir = new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
        if (Physics.Raycast(transform.position, targetDir, out RaycastHit hit, maxDistance))
        {
            if (distance < minDistance)
            {
                walkAnimator.SetBool("Walking",true);
                agent.ResetPath();
                LookAtTarget(targetDir);
                rigidBody.AddForce(-transform.forward * walkSpeed);
            }
            else if (distance < standDistance && hit.collider.gameObject == target)
            {
                walkAnimator.SetBool("Walking", false);
                agent.ResetPath();
                LookAtTarget(targetDir);
                Attack();
            }
            else if (distance < maxDistance && hit.collider.gameObject == target)
            {
                walkAnimator.speed = 1;
                walkAnimator.SetBool("Walking", true);
                LookAtTarget(targetDir);
                Attack();
                agent.SetDestination(target.transform.position);
                agent.speed = walkSpeed;
            }
            else
            {
                walkAnimator.speed = 2;
                walkAnimator.SetBool("Walking", true);
                agent.SetDestination(target.transform.position);
                agent.speed = runSpeed;
            }
        }
        else
        {
            walkAnimator.speed = 2;
            walkAnimator.SetBool("Walking", true);
            agent.SetDestination(target.transform.position);
            agent.speed = runSpeed;
        }
    }

    protected virtual void LookAtTarget(Vector3 targetDir)
    {
        transform.rotation = Quaternion.LookRotation(targetDir, transform.up);
    }

    abstract protected void Attack();
}
