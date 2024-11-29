using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SusIndicator))]
public class WeaponManager : MonoBehaviour,IAnimationReactor
{
    public static WeaponManager Instance { get; private set; }

    const int susLvl = 3;

    [SerializeField] Transform weaponParent;
    Animator animator;

    int[] Ammo = { -1, -1 };

    WeaponSO primary;
    WeaponSO secondary;
    byte current = 0;
    GameObject CurrentGO = null;

    SusIndicator susIndicator;

    bool animationDone = false;
    bool active = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        primary = PlayerDataSingleton.PrimaryW;
        secondary = PlayerDataSingleton.SecondaryW;
        animator = GetComponent<Animator>();
        susIndicator = GetComponent<SusIndicator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && active && current != 1)
            StartCoroutine(Equip(primary, 1));
        else if (Input.GetKeyDown(KeyCode.Alpha2) && active && current != 2)
            StartCoroutine(Equip(secondary, 2));
        else if (Input.GetKeyDown(KeyCode.H) && active && current != 0)
            StartCoroutine(Unequip());
            
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

        BasicGunScript bgs = gun.GetComponent<BasicGunScript>();
        bgs.OnWeaponAwake(x, Ammo[b - 1], animator,true);

        gun.name = "Tool";

        animator.runtimeAnimatorController = x.AnimatorOverrideController;
        animator.SetTrigger("Equip");

        current = b;
        CurrentGO = gun;
        animationDone = false;

        gun.AddComponent<PlayerWeaponInputRelay>();
        
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
            Ammo[current - 1] = CurrentGO.GetComponent<BasicGunScript>().Ammo;
            CurrentGO.GetComponent<BasicGunScript>().OnWeaponDestroy();

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
