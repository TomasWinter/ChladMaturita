using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    int[] code = { 1,2,3,4 };
    int[] input = { 0,0,0,0 };

    int currentInput = 0;

    [SerializeField] TextMeshPro screen;
    [SerializeField] FloatingInteraction FloatingInteraction;

    [SerializeField] TextMeshPro[] codeShowers;

    public UnityEvent CorrectCodeEvent;
    public UnityEvent IncorrectCodeEvent;

    private void Start()
    {
        RandomCode();
        ChangeText();
        ShowCodes();
    }
    public void PressKey(int key)
    {
        if (!enabled)
            return;

        if (key == 10)
        {
            input = new int[]{ 0,0,0,0 };
            currentInput = 0;
            ChangeText();
            StartCoroutine(WaitActivate());
        }
        else if (key == 11)
        {
            currentInput = 0;
            if (IsCorrect())
            {
                CorrectCodeEvent?.Invoke();
                ChangeText(":)");
                enabled = false;
            }
            else
            {
                IncorrectCodeEvent?.Invoke();
                ChangeText(">:(");
                StartCoroutine(WaitActivate(2));
            }
            input = new int[] { 0, 0, 0, 0 };
        }
        else if (currentInput < 4)
        {
            input[currentInput++] = key;
            ChangeText();
            StartCoroutine(WaitActivate());
        }
        else
        {
            StartCoroutine(WaitActivate());
        }
    }

    private void ChangeText(string x = null)
    {
        screen.text = x ?? $"{input[0]}{input[1]}{input[2]}{input[3]}";
    }

    private IEnumerator WaitActivate(float t = 1)
    {
        yield return new WaitForSeconds(t);
        FloatingInteraction.Activate();
    }

    private void RandomCode()
    {
        for (int i = 0; i < 4; i++)
        {
            code[i] = Random.Range(0, 9);
        }
    }

    private void ShowCodes()
    {
        for (int i = 0; i < 4; i++)
        {
            codeShowers[i].text = $"{i+1}-{code[i]}";
        }
    }
    private bool IsCorrect()
    {
        int i = 0;
        foreach (char c in code)
        {
            if (c != input[i++])
                return false;
        }
        return true;
    }
}
