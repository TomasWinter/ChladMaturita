using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    public string Scene;
    public string Description;
    public int Payout;
    public Sprite Image;
}
