using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : EnemyBehavior,IAnimationReactor
{
    [Header("BasicEnemyBehavior")]
    [SerializeField] Transform weaponParent;
    [SerializeField] WeaponSO[] possibleWeapons;
    GunScriptParent currentGunScript;
    
    override protected void Start()
    {
        base.Start();

        WeaponSO currentWeapon = possibleWeapons[(int)Mathf.Round(Random.Range(0f, possibleWeapons.Length - 1f))];
        GameObject gun = Instantiate(currentWeapon.Prefab,weaponParent);

        currentGunScript = gun.GetComponent<GunScriptParent>();
        currentGunScript.OnWeaponAwake(currentWeapon,  -1, animator);

        gun.name = "Tool";

        animator.runtimeAnimatorController = currentWeapon.AnimatorController;
        animator.SetTrigger("Equip");

        for (int i = 0; i < gun.transform.childCount; i++)
        {
            gun.transform.GetChild(i).gameObject.layer = 29;
        }
        gun.layer = 29;
    }
    override protected void Attack()
    {
        currentGunScript.Shoot();
    }
}
