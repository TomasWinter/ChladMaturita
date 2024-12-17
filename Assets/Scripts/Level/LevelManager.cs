using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public UnityEvent BagSecured;
    public UnityEvent PagerAnswered;
    public int Payout { get; private set; } = 0;
    public int RemainingPagers {  get; private set; } = 4;

    private void Awake()
    {
        Instance = this;
    }

    public void SecureLoot(int x)
    {
        if (x == -1)
        {
            PlayerGuiManager.Instance.Secured("x-x");
        }
        else
        {
            Payout += x;
            PlayerGuiManager.Instance.Secured(x.ToString());
            BagSecured?.Invoke();
        }
    }

    public void AnswerPager()
    {
        if (StateManager.Instance?.State == WaveState.Stealth)
            PlayerGuiManager.Instance?.ChangeState($"{RemainingPagers-1}ì", StateManager.WaveStateColors[StateManager.Instance.State], false);
        if (RemainingPagers == 0)
            GlobalEvents.Instance.alarmRaised?.Invoke();
        RemainingPagers--;
        PagerAnswered?.Invoke();
    }
}
