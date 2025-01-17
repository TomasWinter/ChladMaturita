using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [SerializeField][Range(-1f,1f)] float min = 0;
    [SerializeField][Range(-1f, 1f)] float max = 1;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0,Random.Range(0f,360f),0);
        transform.localScale = transform.localScale + (Random.Range(min, max) * Vector3.one);
    }
}
