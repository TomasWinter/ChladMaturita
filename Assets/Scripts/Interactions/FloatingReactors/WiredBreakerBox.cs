using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WiredBreakerBox : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> Wires = new();

    [SerializeField] MeshRenderer OutputWire;
    [SerializeField] Rigidbody Cover;
    [SerializeField] TextMeshPro Screen;

    int correctWire = 0;
    float timer = 0;

    int wiresCut = 0;

    List<string> lines = new()
    {
        "o.o","-.-","o.-","-.o","o~o","o~o","-~-","o~-","-~o","u.u","u-u","u~u","owo","uwu"
    };

    public UnityEvent CoverOpened;
    public UnityEvent BadWireEvent;
    public UnityEvent GoodWireEvent;

    private void Start()
    {
        correctWire = UnityEngine.Random.Range(0, Wires.Count);
        OutputWire.materials = Wires[correctWire].materials;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0)
        {
            ChangeText();
        }
    }

    private void ChangeText(string x = null,int? t = null)
    {
        if (wiresCut >= Wires.Count) 
        {
            Screen.text = Screen.text == "q~q" ? "o~o" : "q~q";
            timer = -0.5f;
        }
        else
        {
            Screen.text = x ?? lines[UnityEngine.Random.Range(0, lines.Count)];
            timer = t ?? -3;
        }
        
    }

    public void CutWire(int wire)
    {
        wiresCut++;
        Wires[wire].gameObject.SetActive(false);
        if (wire == correctWire)
        {
            ChangeText("n.n", -10);
            GoodWireEvent?.Invoke();
        }
        else
        {
            ChangeText("q.q", -10);
            BadWireEvent?.Invoke();
        }
    }

    public void DropCover()
    {
        CoverOpened?.Invoke();
        Cover.isKinematic = false;
    }

    public void AlarmOn()
    {
        GlobalEvents.Instance?.alarmRaised?.Invoke();
    }
}
