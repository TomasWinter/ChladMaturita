using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [SerializeField] bool closeable = false;
    [SerializeField][Range(70, 120)] float angle = 90;
    [SerializeField] FloatingInteraction interactionLeader;

    [SerializeField] bool npcsAllowed = true;

    [SerializeField] AudioClip Squeak;

    bool closed = true;
    NavMeshObstacle obstacle;

    private void Start()
    {
        obstacle = GetComponentInChildren<NavMeshObstacle>();
    }

    public void Interacted(bool forward = false)
    {
        if (enabled)
        {
            enabled = false;
            float desiredAngle = !closed ? 0 : (forward ? -angle : angle);
            closed = !closed;
            obstacle.enabled = !closed || !npcsAllowed;
            StartCoroutine(Animate(desiredAngle));
            AudioManager.Play(gameObject, Squeak, 10, 0.5f, AudioManager.RandomPitch(0.1f));
        }
    }

    private IEnumerator Animate(float desired)
    {
        float original = Mathf.Repeat(transform.localRotation.eulerAngles.x+90,180)-90;
        for (float i = 0;i < 1;i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localRotation = Quaternion.Euler(Anim.ElasticEaseOut(original,desired, i,0.5f), 0, 0);
            
        }
        transform.localRotation = Quaternion.Euler(desired, 0, 0);
        if (closeable)
        {
            enabled = true;
            interactionLeader?.Activate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (npcsAllowed && closed && other.tag != "Player")
        {
            interactionLeader?.Activate(false);
            float x = Vector3.Dot(transform.TransformDirection(Vector3.up), (other.transform.position - transform.position).normalized);
            Interacted(x > 0);
        }
    }
}
