using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerEquipmentData
{
    public static WeaponSO[] PrimaryWeapons {  get; private set; }
    public static WeaponSO[] SecondaryWeapons { get; private set; }
    public static EquipmentSO[] Equipments { get; private set; }

    // Player Data
    public static WeaponSO PrimaryW { get; private set; }
    public static WeaponSO SecondaryW { get; private set; }
    public static EquipmentSO Equipment { get; private set; }

    public static void SetWeapons(WeaponSO primary = null,WeaponSO secondary = null,EquipmentSO equipment = null)
    {
        PrimaryW = primary ?? PrimaryW;
        SecondaryW = secondary ?? SecondaryW;
        Equipment = equipment ?? Equipment;
    }

    public static void SaveEquipment()
    {
        PlayerPrefs.SetInt("PrimaryWeapon", PrimaryW.ID);
        PlayerPrefs.SetInt("SecondaryWeapon", SecondaryW.ID);
        PlayerPrefs.SetInt("Equipment", Equipment.ID);
        PlayerPrefs.Save();
    }

    public static void LoadEquipment()
    {
        if (PlayerPrefs.HasKey("PrimaryWeapon"))
            PrimaryW = PrimaryWeapons.GetById(PlayerPrefs.GetInt("PrimaryWeapon"));
        else
            PrimaryW = PrimaryWeapons.GetById(0);

        if (PlayerPrefs.HasKey("SecondaryWeapon"))
            SecondaryW = SecondaryWeapons.GetById(PlayerPrefs.GetInt("SecondaryWeapon"));
        else
            SecondaryW = SecondaryWeapons.GetById(1);

        if (PlayerPrefs.HasKey("Equipment"))
            Equipment = Equipments.GetById(PlayerPrefs.GetInt("Equipment"));
        else
            Equipment = Equipments.GetById(0);
        
    }

    private static WeaponSO GetById(this WeaponSO[] weapons,int id)
    {
        foreach(WeaponSO weapon in weapons)
        {
            if (weapon.ID == id)
                return weapon;
        }
        return null;
    }

    private static EquipmentSO GetById(this EquipmentSO[] equipments, int id)
    {
        foreach (EquipmentSO equipment in equipments)
        {
            if (equipment.ID == id)
                return equipment;
        }
        return null;
    }

    static PlayerEquipmentData()
    {
        PrimaryWeapons = Resources.LoadAll<WeaponSO>("WeaponScriptableObjects\\Primaries");
        SecondaryWeapons = Resources.LoadAll<WeaponSO>("WeaponScriptableObjects\\Secondaries");
        Equipments = Resources.LoadAll<EquipmentSO>("EquipmentScriptableObjects");
        LoadEquipment();
    }
}