using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LinusServer : MonoBehaviour
{
    [SerializeField] int seconds = 10;

    [SerializeField] TextMeshPro commandLine;
    [SerializeField] Canvas canvas;

    [SerializeField] string LocationString = "solbadboy@linus-server: ";
    [SerializeField] string CommandString = "sudo shutdown";
    [SerializeField] string EndString = "server offline";
    [SerializeField] string[] RandomMessages;

    public UnityEvent FinishedEvent;
    private void Start()
    {
        commandLine.text = LocationString;
    }

    public void Interacted()
    {
        StartCoroutine(HackIt());
    }

    private void Finished()
    {
        FinishedEvent?.Invoke();
    }

    private IEnumerator HackIt()
    {
        canvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Type(CommandString));
        yield return new WaitForSeconds(0.2f);
        commandLine.text += "\n";

        int startingPoint = commandLine.text.Length;
        int messages = 0;
        float nextMessageIn = seconds / RandomMessages.Length;
        commandLine.text += "0.0s - 0%";
        for (float i = 0; i < seconds; i += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);

            if (i >= nextMessageIn*messages && RandomMessages.Length > messages)
            {
                commandLine.text = commandLine.text.Substring(0, startingPoint);
                commandLine.text += RandomMessages[messages] + "\n";

                messages = messages + 1;
                startingPoint = commandLine.text.Length;

                commandLine.text += $"{i:F1}s - {(i / seconds * 100):F0}%";
            }

            commandLine.text = commandLine.text.Substring(0, startingPoint);
            commandLine.text += $"{i:F1}s - {(i / seconds * 100):F0}%";
        }
        commandLine.text = commandLine.text.Substring(0, startingPoint);
        commandLine.text += $"100% - {seconds:D1}s";

        Finished();

        yield return new WaitForSeconds(1);
        commandLine.text += "\n" + EndString;
        yield return new WaitForSeconds(2);
        commandLine.text += "\nShutting down";
        yield return StartCoroutine(Type("...", 0.5f));
        canvas.gameObject.SetActive(false);
    }

    private IEnumerator Type(string line,float delay = 0.05f)
    {
        for (int i = 0; i < line.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            commandLine.text += line[i];
        }
    }
}
