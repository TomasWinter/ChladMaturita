using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Header("Interaction")]
    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] GameObject interactionBarParent;
    [SerializeField] RectTransform interactionBar;
    [Header("Bags")]
    [SerializeField] Image bagBackground;
    [SerializeReference] TextMeshProUGUI bagText;
    [Header("Secured")]
    [SerializeField] RectTransform securedTransform;
    [SerializeField] TextMeshProUGUI securedText;

    int securedCounter = 0;

    private void Awake()
    {
        Instance = this;
        objectiveText = objectiveGui.GetComponentInChildren<TextMeshProUGUI>();
    }
    //Game state
    public void ChangeState(string text,Color color,bool upper = true)
    {
        text = upper ? text.ToUpper() : text;
        stateText.text = text;
        stateText.color = new Color(stateText.color.r, stateText.color.g, stateText.color.b, color.a);
        stateBackground.color = color;
    }
    //Health
    public void ChangeHealth(float health,float maxHealth)
    {
        float y = health / maxHealth;
        HealthBar.localScale = new Vector3(1,y,1);
    }
    //Armor
    public void ChangeArmor(float armor, float maxArmor)
    {
        float y = armor / maxArmor;
        ArmorBar.localScale = new Vector3(1,y,1);
    }
    //Ammo
    public void ChangeAmmo(int ammo,int maxAmmo)
    {
        ammoText.text = ammo.ToString("000");
        maxAmmoText.text = maxAmmo.ToString("000");
    }
    //Objectives
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
    //Interactions
    public void DisplayInteract(bool show,string txt = "",KeyCode k = KeyCode.None)
    {
        interactionText.gameObject.SetActive(show);
        interactionText.text = txt.Replace("#", k.ToString().Split(".").Last().ToUpper());
    }

    public void UpdateInteractBar(bool show,float percent)
    {
        interactionBarParent.SetActive(show);
        interactionBar.localScale = new Vector3(percent,1,1);
    }
    //Bags
    public void SetBag(BagInfo bag)
    {
        if (bag != null)
        {
            bagText.text = bag.Name;
            bagBackground.color = new(0.1176f, 0.1569f, 1.0f);
            StartCoroutine(AnimateBag(true));
        }
        else
        {
            bagText.text = "";
            bagBackground.color = new(0.1020f, 0.1216f, 0.5569f);
            StartCoroutine(AnimateBag(false));
        }
    }

    private IEnumerator AnimateBag(bool hasBag)
    {
        Vector3 increment = new Vector3(Screen.width-200,Screen.height,0)/4000;

        RectTransform rectTransform = bagBackground.GetComponent<RectTransform>();

        if (hasBag)
        {
            for (int i = 0; i < 20; i++)
            {
                rectTransform.position += i*increment;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                rectTransform.position -= i * increment;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    //Loot
    public void Secured(string value)
    {
        securedText.text = $"SECURED: {value}";
        StartCoroutine(AnimateSecured());
    }

    private IEnumerator AnimateSecured()
    {
        securedCounter += 1;
        int s = securedCounter;
        for (float i = 0; i <= 1; i = i + 0.05f)
        {
            securedTransform.localScale = new Vector3(i, 1, 1);
            yield return new WaitForSeconds(0.01f);
            if (securedCounter != s)
                yield break;
        }
        yield return new WaitForSeconds(1);
        if (securedCounter != s)
            yield break;
        for (float i = 1; i >= 0; i = i - 0.05f)
        {
            securedTransform.localScale = new Vector3(i, 1, 1);
            yield return new WaitForSeconds(0.01f);
            if (securedCounter != s)
                yield break;
        }
    }
}
