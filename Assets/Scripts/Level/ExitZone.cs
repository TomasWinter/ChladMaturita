using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitZone : MonoBehaviour
{
    bool isActive = false;
    float timer = 0;

    public UnityEvent Escaped;

    private void Start()
    {
        gameObject.SetActive(isActive);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && isActive)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                MissionOverScript.Instance.Show(true, "YAY");
                Escaped?.Invoke();
            }
        }
    }

    public void Enable(bool b)
    {
        isActive = b;
        gameObject.SetActive(b);
    }
}
