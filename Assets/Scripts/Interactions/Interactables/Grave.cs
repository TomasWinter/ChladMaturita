using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : Loot
{
    [SerializeField] AudioClip ringing;
    bool hasAnswered = false;

    protected override void Start()
    {
        Info = BagInfo.GetInfo(LootType.Body);
        
    }

    public void SetPager(bool must)
    {
        hasAnswered = !must;
        if (hasAnswered)
            Answer();
        else
            StartCoroutine(Countdown());
    }

    public override void Interacted(bool b = true)
    {
        if (hasAnswered)
            base.Interacted(b);
        else
        {
            LevelManager.Instance?.AnswerPager();
            Answer();
        }
    }
    public override void IsInteracting(bool b)
    {
        base.IsInteracting(b);
        if (b)
            AudioManager.StopLoop(gameObject);
    }
    private void Answer()
    {
        IsEnabled = false;
        InteractedEvent?.Invoke();

        hasAnswered = true;
        interactionTime = 0.5f;
        interactionText = "pick up";

        StartCoroutine(Answered());
    }
    private IEnumerator Answered()
    {
        yield return new WaitForSeconds(0.1f);

        IsEnabled = true;
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);

        IsEnabled = true;
        GameObject billboard = BillboardManager.TextBillboard("ì", Color.red, transform.position, transform);
        AudioManager.PlayLoop(gameObject, ringing, 50);

        yield return new WaitForSeconds(5);

        AudioManager.StopLoop(gameObject);

        yield return new WaitUntil(() => !interacting);
        
        BillboardManager.DestroyBillboard(billboard);

        if (!hasAnswered)
        {
            SetOffAlarm();
            Answer();
        }
    }

    private void SetOffAlarm()
    {
        GlobalEvents.Instance.alarmRaised?.Invoke();
    }
}
