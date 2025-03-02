using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanScript : MonoBehaviour
{
    [SerializeField] GameObject extractionZone;

    Animator animator;

    private void Start()
    {
        extractionZone?.SetActive(false);

        animator = GetComponent<Animator>();
    }

    public void Arrive(string animName)
    {
        animator.Play(animName, 0);
    }

    public void VanArrived()
    {
        extractionZone?.SetActive(true);
    }
}
