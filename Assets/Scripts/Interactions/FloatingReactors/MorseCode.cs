using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MorseCode : MonoBehaviour
{
    [SerializeField] TextMeshPro paper;
    [SerializeField] MeshRenderer indicatorLed;
    [SerializeField] MeshRenderer successLed;
    [SerializeField] Material materialOn;
    [SerializeField] Material materialOff;
    [SerializeField] Material materialWrong;
    [SerializeField] Material materialRight;

    List<string> morseTranslation = new List<string> { "...." };
    string colorSequence = "";
    string inputSequence = "";

    float timer = 2f;

    bool write = true;
    int currentChar = 0;

    const float dotDuration = 0.5f;

    public UnityEvent ButtonPressedEvent;
    public UnityEvent WrongEvent;
    public UnityEvent CorrectEvent;
    public UnityEvent EnableButtonsEvent;

    private void Start()
    {
        SetDefaultInput();
        GenerateDictionaryField();
        GenerateDictionaryField();
        GenerateDictionaryField();
        GenerateDictionaryField();

        paper.text =
            $"{morseTranslation[1]} = <color=blue>blue</color>\r\n" +
            $"{morseTranslation[2]} = <color=red>red</color>\r\n" +
            $"{morseTranslation[3]} = <color=yellow>yellow</color>\r\n" +
            $"{morseTranslation[4]} = <color=green>green</color>\r\n" +
            $"{morseTranslation[0]} = start";

        colorSequence = GenerateColorSequence();
        indicatorLed.material = materialOff;
        successLed.material = materialOff;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            successLed.material = materialOff;
            if (!write || colorSequence[currentChar] == '|')
            {
                indicatorLed.material = materialOff;
                
                if (++currentChar >= colorSequence.Length)
                {
                    currentChar = 0;
                    timer = dotDuration * 5;
                }
                else
                    timer = dotDuration;
                write = true;
            }
            else
            {
                indicatorLed.material = materialOn;
                timer = colorSequence[currentChar] == '.' ? dotDuration : dotDuration * 3;
                write = false;
            }
        }
    }

    public void PressButton(int color)
    {
        inputSequence += "|"+morseTranslation[color];
        Debug.Log($"\r\n{colorSequence}\r\n{inputSequence}");
        if (inputSequence.Length == colorSequence.Length)
        {
            if (inputSequence == colorSequence)
            {
                successLed.material = materialRight;
                timer = float.MaxValue;
                enabled = false;
                CorrectEvent?.Invoke();
                return;
            }
            else
            {
                SetDefaultInput();
                WrongEvent?.Invoke();
            }
        }
        ButtonPressedEvent?.Invoke();
        StartCoroutine(delayPress());
    }

    private IEnumerator delayPress()
    {
        yield return new WaitForSeconds(0.3f);
        EnableButtonsEvent?.Invoke();
    }

    private void SetDefaultInput()
    {
        inputSequence = morseTranslation[0];
        successLed.material = materialWrong;
        write = false;
        currentChar = -1;
        timer = 2;
    }

    private string GenerateColorSequence()
    {
        string output = morseTranslation[0];
        for (int i = 0; i < 4; i++)
        {
            output += "|";
            int Num = Random.Range(1, 5);
            switch (Num)
            {
                case 1:
                    output += morseTranslation[1];
                    break;
                case 2:
                    output += morseTranslation[2];
                    break;
                case 3:
                    output += morseTranslation[3];
                    break;
                case 4:
                    output += morseTranslation[4];
                    break;
            }
        }
        return output;
    }

    private void GenerateDictionaryField()
    {
        string sequence;
        while (true)
        {
            sequence = GenerateSequence(4);
            if (!morseTranslation.Contains(sequence))
                break;
        }
        morseTranslation.Add(sequence);
    }

    private string GenerateSequence(int length)
    {
        string sequence = "";
        for (int i = 0;i < length;i++)
        {
            sequence += Random.Range(0, 2) == 0 ? "." : "-";
        }
        return sequence;
    }

    
}