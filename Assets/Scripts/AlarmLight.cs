using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    [SerializeField] float alarmTime = 10;
    [SerializeField] float speed = 1;
    [SerializeField] float volume = 1;
    [SerializeField] bool turnOff = false;
    [SerializeField] AudioClip audioClip;

    Light[] lights;

    private void Start()
    {
        lights = gameObject.GetComponentsInChildren<Light>();
        foreach (Light light in lights)
        {
            light.enabled = false;
        }

        GlobalEvents.Instance?.alarmRaised?.AddListener(SoundAlarm);
    }

    private void SoundAlarm()
    {
        GlobalEvents.Instance?.alarmRaised?.RemoveListener(SoundAlarm);
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
        AudioManager.PlayLoop(gameObject, audioClip, float.MaxValue, volume, 1);
        StartCoroutine(Alarming());
    }

    private IEnumerator Alarming()
    {
        for (float i = 0; i < alarmTime; i += Time.deltaTime)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z + (speed * Time.deltaTime));
            yield return null;
        }
        AudioManager.StopLoop(gameObject);
        if (turnOff)
        {
            yield return new WaitForSeconds(2);
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
        }
        
    }
}
