using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGuiManager : MonoBehaviour
{
    public static PlayerGuiManager Instance { get; private set; }

    [Header("Health")]
    [SerializeField] RectTransform HealthBar;
    [SerializeField] RectTransform ArmorBar;
    [Header("Ammo")]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI maxAmmoText;
    [Header("Objectives")]
    [SerializeField] RectTransform objectiveGui;
    TextMeshProUGUI objectiveText;
    [Header("Game State")]
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] Image stateBackground;

    private void Awake()
    {
        Instance = this;
        objectiveText = objectiveGui.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void ChangeState(string text,Color color)
    {
        stateText.text = text.ToUpper();
        stateText.color = new Color(stateText.color.r, stateText.color.g, stateText.color.b, color.a);
        stateBackground.color = color;
    }
    public void ChangeHealth(float health,float maxHealth)
    {
        float y = health / maxHealth;
        HealthBar.localScale = new Vector3(1,y,1);
    }

    public void ChangeArmor(float armor, float maxArmor)
    {
        float y = armor / maxArmor;
        ArmorBar.localScale = new Vector3(1,y,1);
    }

    public void ChangeAmmo(int ammo,int maxAmmo)
    {
        ammoText.text = ammo.ToString("000");
        maxAmmoText.text = maxAmmo.ToString("000");
    }

    public void ChangeObjective(ObjectiveParent op,bool instant = false)
    {
        if (op == null)
        {
            ObjectiveCompleted(null, false);
        }
        else if (op is CountObjective oc && oc.showCount)
        {
            ObjectiveCompleted($"{oc.Text} {oc.timeDone}/{oc.required}", instant);
        }
        else
        {
            ObjectiveCompleted(op.Text, instant);
        } 
    }

    private void ObjectiveCompleted(string txt, bool instant)
    {
        if (txt == null)
        {
            objectiveText.color = new Color(0, 0.8f, 0, 1);
        }
        else if (instant)
        {
            objectiveGui.sizeDelta = new(txt.Length * 12.5f + 30, objectiveGui.sizeDelta.y);
            objectiveText.text = txt;
            objectiveText.color = new Color(1, 1, 1, 1);
        }
        else
        {
            StartCoroutine(ChangeObjText(txt));
        }
    }

    private IEnumerator ChangeObjText(string txt)
    {
        Image squareColor = objectiveGui.gameObject.GetComponent<Image>();
        objectiveText.color = new(0, 180, 0);
        for (int i = 100; i > 0f; i--)
        {
            squareColor.color -= new Color(0, 0, 0, 0.01f);
            objectiveText.color -= new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        objectiveGui.sizeDelta = new(txt.Length * 12.5f + 30, objectiveGui.sizeDelta.y);
        objectiveText.text = txt;
        objectiveText.color = new(1,1,1,0);
        for (int i = 0; i < 100; i++)
        {
            squareColor.color += new Color(0, 0, 0, 0.01f);
            objectiveText.color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
