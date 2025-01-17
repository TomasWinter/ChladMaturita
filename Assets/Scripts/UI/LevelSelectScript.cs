using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
{
    [SerializeField] List<LevelSO> levels = new List<LevelSO>();
    [SerializeField] GameObject Template;
    [SerializeField] Transform Transform;

    private void Start()
    {
        foreach (LevelSO level in levels)
        {
            CreateLvl(level);
        }
    }

    public void LoadScene(LevelSO lvlSO)
    {
        SceneManager.LoadScene(lvlSO.Scene);
    }

    private void CreateLvl(LevelSO lvlSO)
    {
        GameObject x = Instantiate(Template);
        x.transform.SetParent(Transform, false);

        x.transform.Find("Panel/Image").GetComponent<Image>().sprite = lvlSO.Image;
        x.transform.Find("Panel/Title").GetComponent<TextMeshProUGUI>().text = lvlSO.Scene;
        x.transform.Find("Panel/Description").GetComponent<TextMeshProUGUI>().text = lvlSO.Description;
        x.transform.Find("Panel/Payout").GetComponent<TextMeshProUGUI>().text = "Payout: " + lvlSO.Payout;

        EventTrigger et = x.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((d) => LoadScene(lvlSO));
        et.triggers.Add(entry);

        et = x.GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((d) => ButtonScript.Instance.Darken(x.GetComponent<Image>()));
        et.triggers.Add(entry);

        et = x.GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((d) => ButtonScript.Instance.Lighten(x.GetComponent<Image>()));
        et.triggers.Add(entry);
    }
}