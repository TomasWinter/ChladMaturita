using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SusIndicator))]
public class WeaponManager : MonoBehaviour,IAnimationReactor
{
    public static WeaponManager Instance { get; private set; }

    const int susLvl = 3;

    [SerializeField] Transform weaponParent;
    [Header("Audio")]
    [SerializeField] AudioClip equipSound;
    Animator animator;

    int[] Ammo = { -1, -1 };

    WeaponSO primary;
    WeaponSO secondary;
    int current = 0;
    GameObject CurrentGO = null;

    EquipmentSO equipment;
    int uses = 0;

    SusIndicator susIndicator;

    bool animationDone = false;
    bool active = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        primary = PlayerEquipmentData.PrimaryW;
        secondary = PlayerEquipmentData.SecondaryW;
        equipment = PlayerEquipmentData.Equipment;
        uses = equipment.Uses;

        animator = GetComponent<Animator>();
        susIndicator = GetComponent<SusIndicator>();

        PlayerGuiManager.Instance?.ChangeEquipment(equipment.Uses, equipment.EquipmentImage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(Settings.Keybinds.Weapon1) && active && current != 1)
            StartCoroutine(Equip(primary, 1));
        else if (Input.GetKeyDown(Settings.Keybinds.Weapon2) && active && current != 2)
            StartCoroutine(Equip(secondary, 2));
        else if (Input.GetKeyDown(Settings.Keybinds.HideWeapon) && active && current != 0)
            StartCoroutine(Unequip());
        else if (Input.GetKeyDown(Settings.Keybinds.Equipment) && active && uses > 0)
            DeployEquipemnt();
    }

    private void DeployEquipemnt()
    {
        if (Physics.Raycast(PlrCameraScript.Instance.transform.position, PlrCameraScript.Instance.transform.forward, out RaycastHit hit, 5))
        {
            uses--;
            Instantiate(equipment.Prefab,hit.point + new Vector3(0,0.25f,0),Quaternion.identity);
            PlayerGuiManager.Instance?.ChangeEquipment(uses);
        }
    }

    private IEnumerator Equip(WeaponSO x,byte b)
    {
        if (current != 0)
        {
            yield return StartCoroutine(Unequip());
            yield return new WaitForSeconds(0.1f);
        }
        active = false;
        GameObject gun = Instantiate(x.Prefab, weaponParent);

        GunScriptParent bgs = gun.GetComponent<GunScriptParent>();
        bgs.OnWeaponAwake(x, Ammo[b - 1], animator,true);

        gun.name = "Tool";

        animator.runtimeAnimatorController = x.AnimatorController;
        animator.SetTrigger("Equip");

        current = b;
        CurrentGO = gun;
        animationDone = false;

        gun.AddComponent<PlayerWeaponInputRelay>();

        AudioManager.Play(gameObject,equipSound,3);
        yield return new WaitUntil(() => animationDone);
        
        susIndicator.SusLvl = susLvl;
        active = true;
    }

    private IEnumerator Unequip()
    {
        if (CurrentGO != null)
        {
            active = false;
            animator.SetTrigger("Unequip");
            animationDone = false;
            Ammo[current - 1] = CurrentGO.GetComponent<GunScriptParent>().Ammo;
            CurrentGO.GetComponent<GunScriptParent>().OnWeaponDestroy();

            AudioManager.PlayReversed(gameObject, equipSound, 3);
            yield return new WaitUntil(() => animationDone);

            Destroy(CurrentGO);
            current = 0;
            CurrentGO = null;
            animator.runtimeAnimatorController = null;
            active = true;
            susIndicator.SusLvl = -susLvl;
        }
    }

    public void AnimDone(string name = "")
    {
        if (name == "Equip" || name == "Unequip")
            animationDone = true;
    }
}
