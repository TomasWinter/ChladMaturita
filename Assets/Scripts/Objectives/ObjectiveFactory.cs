using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public static class ObjectiveFactory
{
    public static ObjectiveParent Assemble(ObjectiveTemplate objT)
    {
        UnityEvent ue = new();
        if (objT.mono != null)
        {
            FieldInfo[] fields = objT.mono.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.Name == objT.eventName)
                {
                    ue = (UnityEvent)field.GetValue(objT.mono);
                }
            }
        }
        switch (objT.type)
        {
            case ObjectiveType.Simple:
                return new SimpleObjective(objT.text,objT.description,ue,objT.waypointInfo);
            case ObjectiveType.Count:
                return new CountObjective(objT.text,objT.description,ue,objT.amount,objT.active, objT.waypointInfo);
            case ObjectiveType.Find:
                return new FindObjective(objT.text, objT.description, ue, objT.amount, objT.waypointInfo);
            case ObjectiveType.Wait:
                return new WaitObjective(objT.text, objT.description, objT.amount);
            case ObjectiveType.Walk:
                return new WalkObjective(objT.text, objT.description, objT.collider, objT.waypointInfo);
            default:
                Debug.LogWarning("Type of objective is missing!");
                return new SimpleObjective(objT.text, objT.description, ue, objT.waypointInfo);
        }
    }
}

public enum ObjectiveType
{
    Simple,
    Count,
    Find,
    Wait,
    Walk
}