using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGlobalSetter : MonoBehaviour
{
    [SerializeField] WeaponSO PdsPrimary;
    [SerializeField] WeaponSO PdsSecondary;
    private void Awake()
    {
        Debug.LogWarning("You are setting the loadout using a debug script");
        if (PlayerEquipmentData.PrimaryW == null || PlayerEquipmentData.SecondaryW == null)
        {
            PlayerEquipmentData.SetWeapons(PdsPrimary, PdsSecondary);
        }
    }
}
