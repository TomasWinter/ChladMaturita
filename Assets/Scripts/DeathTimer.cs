using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float dieTime = 10;
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dieTime)
            Destroy(gameObject);
    }
}
