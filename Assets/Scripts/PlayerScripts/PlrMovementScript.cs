using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlrMovementScript : MonoBehaviour
{
    public static PlrMovementScript Instance {  get; private set; }

    public MovementState MoveState { get; private set; }

    [SerializeField] float speed = 1;
    [SerializeField] float jumpPower = 1;

    List<float> speedModifiers = new();

    Rigidbody rb;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 dir = Vector3.zero;
        float speedmod = Mathf.Clamp(1f - speedModifiers.Sum(), 0.05f, 10f);
        bool sprinting = false;

        if (Input.GetKey(KeyCode.W))
            dir.z = 1;
        else if (Input.GetKey(KeyCode.S))
            dir.z = -1;

        if (Input.GetKey(KeyCode.D))
            dir.x = 1;
        else if (Input.GetKey(KeyCode.A))
            dir.x = -1;

        if (Input.GetKey(KeyCode.LeftShift))
        { 
            speedmod *= 2; 
            sprinting = true;
        }

        if (Physics.Raycast(transform.position, -transform.up, 1.1f))
        {
            rb.drag = 5;
            if (Input.GetKey(KeyCode.Space))
            {
                speedmod *= 0.05f;
                rb.drag = 0;
                rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
                MoveState = MovementState.Airborne;
            }
            else
            {
                if (dir != Vector3.zero)
                {
                    if (sprinting)
                        MoveState = MovementState.Run;
                    else
                        MoveState = MovementState.Walk;
                }
                else
                    MoveState = MovementState.Stand;
            }
        }
        else
        {
            MoveState = MovementState.Airborne;
            speedmod *= 0.05f;
            rb.drag = 0;
        }

        Vector3 finalForce = dir.normalized * speed * speedmod;
        rb.AddRelativeForce(finalForce, ForceMode.Force);

    }

    public void AddSpeedModifier(float speed)
    {
        speedModifiers.Add(speed);
    }
    public void RemoveSpeedModifier(float speed)
    {
        speedModifiers.Remove(speed);
    }
}

public enum MovementState
{
    Airborne = 0,
    Stand = 1,
    Walk = 5,
    Run = 10
}