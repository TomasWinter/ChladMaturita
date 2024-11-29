using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPool", menuName = "ScriptableObjects/SpawnPool")]
public class SpawnPoolSO : ScriptableObject
{
    public List<GameObjectProbability> fields = new List<GameObjectProbability>();
}

[Serializable]
public class GameObjectProbability
{
    public GameObject Prefab;
    public int Probability;
}
