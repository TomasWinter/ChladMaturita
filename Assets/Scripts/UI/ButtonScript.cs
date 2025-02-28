using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public static ButtonScript Instance { get; private set; }
    const int biggerAmount = 10;
    Dictionary<TextMeshProUGUI, bool> Highlighted = new Dictionary<TextMeshProUGUI, bool>();

    public void Awake()
    {
        Instance = this;
    }

    public void Highlight(TextMeshProUGUI tmp)
    {
        //tmp.text = "-" + tmp.text + "-";
        StartCoroutine(Bigger(tmp));
    }

    private IEnumerator Bigger(TextMeshProUGUI tmp)
    {
        if (Highlighted.ContainsKey(tmp))
        {
            Highlighted[tmp] = false;
            yield return new WaitUntil(() => !Highlighted.ContainsKey(tmp));
        }
        Highlighted.Add(tmp, true);
        float original = tmp.fontSize;
        for (int i = 0;i < biggerAmount / 1f && Highlighted[tmp];i++)
        {
            tmp.fontSize += 1f;
            yield return new WaitForSeconds(0.01f);
        }
        tmp.fontSize = original + biggerAmount;
        Highlighted.Remove(tmp);
    }

    public void Unhighlight(TextMeshProUGUI tmp)
    {
        //tmp.text = tmp.text.Replace("-", "");
        StartCoroutine(Smaller(tmp));
    }

    private IEnumerator Smaller(TextMeshProUGUI tmp)
    {
        if (Highlighted.ContainsKey(tmp))
        {
            Highlighted[tmp] = false;
            yield return new WaitUntil(() => !Highlighted.ContainsKey(tmp));
        }
        Highlighted.Add(tmp, true);
        float original = tmp.fontSize;
        for (int i = 0; i < biggerAmount / 1f && Highlighted[tmp]; i++)
        {
            tmp.fontSize -= 1f;
            yield return new WaitForSeconds(0.01f);
        }
        tmp.fontSize = original - biggerAmount;
        Highlighted.Remove(tmp);
    }

    public void Darken(Image img)
    {
        img.color = img.color - new Color(0.1f,0.1f,0.1f,0);
    }

    public void Lighten(Image img)
    {
        img.color = img.color + new Color(0.1f, 0.1f, 0.1f, 0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}