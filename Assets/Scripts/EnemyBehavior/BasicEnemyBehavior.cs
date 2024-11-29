using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : EnemyBehavior,IAnimationReactor
{
    [Header("BasicEnemyBehavior")]
    [SerializeField] Transform weaponParent;
    [SerializeField] WeaponSO[] possibleWeapons;
    BasicGunScript currentGunScript;
    
    override protected void Start()
    {
        base.Start();

        WeaponSO currentWeapon = possibleWeapons[(int)Mathf.Round(Random.Range(0f, possibleWeapons.Length - 1f))];
        GameObject gun = Instantiate(currentWeapon.Prefab,weaponParent);
        gun.layer = 29;

        currentGunScript = gun.GetComponent<BasicGunScript>();
        currentGunScript.OnWeaponAwake(currentWeapon,  -1, animator);

        gun.name = "Tool";

        animator.runtimeAnimatorController = currentWeapon.AnimatorOverrideController;
        animator.SetTrigger("Equip");
    }
    override protected void Attack()
    {
        currentGunScript.Shoot();
    }
}
