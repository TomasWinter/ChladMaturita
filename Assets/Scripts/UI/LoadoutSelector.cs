using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutSelector : MonoBehaviour
{
    [SerializeField] GameObject primary;
    [SerializeField] GameObject secondary;
    [SerializeField] GameObject equipment;
    public void ChangePrimary(int move)
    {
        int i = PlayerEquipmentData.PrimaryWeapons.ToList().IndexOf(PlayerEquipmentData.PrimaryW);
        i = (int)Mathf.Repeat(i + move, PlayerEquipmentData.PrimaryWeapons.Length);
        PlayerEquipmentData.SetWeapons(PlayerEquipmentData.PrimaryWeapons[i],null);
        ChangeSprites();
    }

    public void ChangeSecondary(int move)
    {
        int i = PlayerEquipmentData.SecondaryWeapons.ToList().IndexOf(PlayerEquipmentData.SecondaryW);
        i = (int)Mathf.Repeat(i + move, PlayerEquipmentData.SecondaryWeapons.Length);
        PlayerEquipmentData.SetWeapons(null, PlayerEquipmentData.SecondaryWeapons[i]);
        ChangeSprites();
    }

    public void ChangeEquipment(int move)
    {
        int i = PlayerEquipmentData.Equipments.ToList().IndexOf(PlayerEquipmentData.Equipment);
        i = (int)Mathf.Repeat(i + move, PlayerEquipmentData.Equipments.Length);
        PlayerEquipmentData.SetWeapons(null, null, PlayerEquipmentData.Equipments[i]);
        ChangeSprites();
    }

    private void ChangeSprites()
    {
        primary.transform.Find("PrimaryImg").GetComponent<Image>().sprite = PlayerEquipmentData.PrimaryW.GunImage;
        primary.GetComponentInChildren<TextMeshProUGUI>().text = PlayerEquipmentData.PrimaryW.name;

        secondary.transform.Find("SecondaryImg").GetComponent<Image>().sprite = PlayerEquipmentData.SecondaryW.GunImage;
        secondary.GetComponentInChildren<TextMeshProUGUI>().text = PlayerEquipmentData.SecondaryW.name;

        equipment.transform.Find("EquipmentImg").GetComponent<Image>().sprite = PlayerEquipmentData.Equipment.EquipmentImage;
        equipment.GetComponentInChildren<TextMeshProUGUI>().text = PlayerEquipmentData.Equipment.name;
    }

    public void Save()
    {
        PlayerEquipmentData.SaveEquipment();
    }

    private void Start()
    {
        ChangeSprites();
    }
}
