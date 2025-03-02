using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedZone : MonoBehaviour
{
    [SerializeField] sbyte SusLvl = 3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<SusIndicator>() != null)
        {
            other.GetComponent<SusIndicator>().SusLvl = SusLvl;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<SusIndicator>() != null)
        {
            other.GetComponent<SusIndicator>().SusLvl = (sbyte)-SusLvl;
        }
    }
}
