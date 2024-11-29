using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] List<ObjectiveTemplate> ObjectiveTemplates;
    List<ObjectiveParent> Objectives = new();

    int currentObj = 0;
    private void Start()
    {
        foreach (ObjectiveTemplate template in ObjectiveTemplates)
        {
            Objectives.Add(ObjectiveFactory.Assemble(template));
        }
        ObjectiveTemplates.Clear();

        PlayerGuiManager.Instance.ChangeObjective(Objectives[currentObj],true);
        Objectives[currentObj].Initiate(ObjCompleted);
    }

    public void ObjCompleted()
    {
        currentObj++;
        if (currentObj < Objectives.Count)
        {
            PlayerGuiManager.Instance.ChangeObjective(Objectives[currentObj]);
            Objectives[currentObj].Initiate(ObjCompleted);
        }
        else
        {
            PlayerGuiManager.Instance.ChangeObjective(null);
        }
    }
}
