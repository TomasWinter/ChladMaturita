using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGlobalSetter : MonoBehaviour
{
    [SerializeField] WeaponSO PdsPrimary;
    [SerializeField] WeaponSO PdsSecondary;
    private void Awake()
    {
        PlayerDataSingleton.PrimaryW = PdsPrimary;
        PlayerDataSingleton.SecondaryW = PdsSecondary;
    }
}
