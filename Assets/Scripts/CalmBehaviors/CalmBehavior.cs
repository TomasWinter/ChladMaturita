using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public abstract class CalmBehavior : MonoBehaviour, IDieOff
{
    [SerializeField] [Range(0,180)] float fov = 45;
    [SerializeField] float detectionSpeed = 0.1f;
    [SerializeField] float radius = 0;
    [SerializeField] LayerMask layerMask;

    protected bool active = true;
    protected float alertLvl = 0;
    protected sbyte DetectionLvl = 1;

    protected TextMeshProUGUI alertText;
    protected RectTransform alertRect;
    protected Canvas alertCanvas;

    protected HealthScriptParent GHS = null;
    protected void Start()
    {
        alertText = GetComponentInChildren<TextMeshProUGUI>();
        alertRect = alertText.GetComponentsInChildren<RectTransform>().Last();
        alertCanvas = alertText.transform.parent.GetComponent<Canvas>();
        GlobalEvents.Instance.alarmRaised?.AddListener(AlarmRaised);

        GHS = GetComponent<HealthScriptParent>();
        if (GHS != null)
            GetComponent<HealthScriptParent>().hurtEvent.AddListener(Hurt);
    }
    protected virtual void Update()
    {
        if (!active || alertLvl >= 100) { return; }

        Collider[] CurrentlyInRange = Physics.OverlapSphere(transform.position, radius,layerMask);
        if (CurrentlyInRange.Length > 1)
        {
            SusIndicator foundCandidate = null;
            float distance = float.MaxValue;
            foreach (Collider collider in CurrentlyInRange)
            {
                SusIndicator si = collider.gameObject.GetComponent<SusIndicator>();
                if (si != null && si.SusLvl >= DetectionLvl && (foundCandidate != null || distance > (transform.position - collider.transform.position).magnitude))
                {
                    if (Vector3.Angle(transform.forward,(collider.transform.position - transform.position)) < fov)
                    {
                        if (Physics.Raycast(transform.position, (collider.transform.position - transform.position), out RaycastHit hit, radius) && hit.collider.gameObject == collider.gameObject)
                        {
                            foundCandidate = si;
                            distance = hit.distance;
                        }
                    }
                }
            }
            if (foundCandidate != null)
                alertLvl = Mathf.Clamp((detectionSpeed/distance) + alertLvl, 0, 100);
            else
                alertLvl = Mathf.Clamp(alertLvl - detectionSpeed * 0.5f, 0, 100);
        }
        else
            alertLvl = Mathf.Clamp(alertLvl - detectionSpeed * 0.5f, 0, 100);
        
        if (alertLvl > 0)
        {
            alertRect.localScale = new Vector3(1,alertLvl/100,1);
            if (alertLvl >= 100)
            {
                alertText.text = "!";
                StartCoroutine(DetectedSomething());
            }
        }
        alertCanvas.enabled = alertLvl > 0;
    }

    protected abstract IEnumerator DetectedSomething();
    public abstract void AlarmRaised();

    protected void Hurt()
    {
        GetComponent<HealthScriptParent>().hurtEvent.RemoveListener(Hurt);
        alertLvl = 9001;
        alertCanvas.enabled = true;
        alertText.text = "!";
        alertRect.localScale = new Vector3(1, 1, 1);
        StartCoroutine(DetectedSomething());
    }

    public void Shutdown()
    {
        if (GHS != null)
            GetComponent<HealthScriptParent>().hurtEvent.RemoveListener(Hurt);
        alertText.enabled = false;
        active = false;
        this.enabled = false;
    }
}
