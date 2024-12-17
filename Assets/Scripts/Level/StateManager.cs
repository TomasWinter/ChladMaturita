using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static Dictionary<WaveState, string> WaveStateStrings { get; private set; } = new Dictionary<WaveState, string>()
    {
        { WaveState.Stealth, "Concealed" },
        { WaveState.Break, "Assault break" },
        { WaveState.Loud, "Assault in progress" }
    };
    public static Dictionary<WaveState, Color> WaveStateColors { get; private set; } = new Dictionary<WaveState, Color>()
    {
        { WaveState.Stealth, new Color(0.2103878f, 0.1832503f, 0.5471698f, 0.8f) },
        { WaveState.Break, new Color(0.8117647f, 0.5820678f, 0.1568627f, 0f) },
        { WaveState.Loud, new Color(0.8117647f, 0.5820678f, 0.1568627f, 1f) }
    };
    public static StateManager Instance {  get; private set; }
    public WaveState State { get; private set; }

    private void Awake()
    {
        Instance = this;
        State = WaveState.Stealth;
    }

    float timer = 0;
    [SerializeField] float breakTime = 10;
    [SerializeField] float assaultTime = 100;

    private void Start()
    {
        if (State == WaveState.Stealth)
            PlayerGuiManager.Instance.ChangeState("4ì", WaveStateColors[State],false);
        else
            PlayerGuiManager.Instance.ChangeState(WaveStateStrings[State], WaveStateColors[State]);

        GlobalEvents.Instance.alarmRaised?.AddListener(GoLoud);
    }

    private void Update()
    {
        if (timer > breakTime && State == WaveState.Break)
        {
            State = WaveState.Loud;
            timer = 0;
            PlayerGuiManager.Instance.ChangeState(WaveStateStrings[State], WaveStateColors[State]);
        }
        else if (timer > assaultTime && State == WaveState.Loud)
        {
            State = WaveState.Break;
            timer = 0;
            PlayerGuiManager.Instance.ChangeState(WaveStateStrings[State], WaveStateColors[State]);
        }
        else if (State != WaveState.Stealth)
            timer += Time.deltaTime;
    }

    private void GoLoud()
    {
        GlobalEvents.Instance.alarmRaised?.RemoveListener(GoLoud);
        State = WaveState.Break;
        PlayerGuiManager.Instance.ChangeState(WaveStateStrings[State], WaveStateColors[State]);
    }
}
