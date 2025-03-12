using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Image;

public class BreakerBox : MonoBehaviour
{
    [SerializeField] float openTime = 3;
    [SerializeField] float angle = 90;

    [SerializeField] Vector3 rotationVector = Vector3.zero;

    [SerializeField] EasingStyle easingStyle = EasingStyle.Linear;

    public UnityEvent OpenedEvent;
    public void Open()
    {
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        float original = Mathf.Repeat(transform.localRotation.eulerAngles.x + 90, 180) - 90;
        for (float i = 0; i < 1; i += 1 / (100 * openTime))
        {
            yield return new WaitForSeconds(0.01f);
            Vector3 eulerAngle = rotationVector * CalculateEasing(original,i);
            transform.localRotation = Quaternion.Euler(eulerAngle);

        }
        transform.localRotation = Quaternion.Euler(rotationVector * angle);
        OpenedEvent?.Invoke();
    }

    private float CalculateEasing(float original,float t)
    {
        switch (easingStyle)
        {
            case EasingStyle.SineInOut:
                return Anim.SineInOut(original, angle, t);
            case EasingStyle.ElasticInOut:
                return Anim.ElasticOut(original, angle, t, 0.5f);
            case EasingStyle.ElasticIn:
                return Anim.ElasticIn(original, angle, t, 0.5f);
            default:
                return Anim.Linear(original, angle, t);
        }
    }
}
