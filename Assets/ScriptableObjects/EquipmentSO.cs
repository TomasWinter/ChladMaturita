using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObjects/Equipment")]
public class EquipmentSO : ScriptableObject
{
    private static int LastID = 0;

    public int ID = -1;
    public int Uses = 1;
    public GameObject Prefab;
    public Sprite EquipmentImage;

    private void OnValidate()
    {
        if (ID == -1)
        {
            ID = LastID++;
        }
    }
}
