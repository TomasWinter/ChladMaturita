using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSingleton : MonoBehaviour
{
    public static WeaponSO PrimaryW { get; set; }
    public static WeaponSO SecondaryW { get; set; }
}
