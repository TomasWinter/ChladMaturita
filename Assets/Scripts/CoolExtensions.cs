using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CoolExtensions
{
    public static GameObjectProbability GetRandom(this List<GameObjectProbability> x)
    {
        if (x.Count == 0)
        {
            Debug.LogWarning("list was empty");
            return null;
        }

        int totalProbability = 0;
        List<int> arrayProbability = new List<int>();

        foreach (GameObjectProbability field in x)
        {
            totalProbability += field.Probability;
            arrayProbability.Add(totalProbability);
        }

        int randomSelected = Random.Range(0, totalProbability);

        int i = 0;
        foreach (int prob in arrayProbability)
        {
            if (prob > randomSelected)
            {
                //Debug.Log($"Total:{totalProbability}; Selected:{randomSelected}; i:{i}");
                return x[i];
            }
            i++;
        }
        
        return null;
    }
}
