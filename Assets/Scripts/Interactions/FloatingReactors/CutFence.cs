using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutFence : MonoBehaviour
{
    [SerializeField] GameObject opened;
    [SerializeField] GameObject closed;
    public void Interacted()
    {
        closed.SetActive(false);
        opened.SetActive(true);
    }
}
