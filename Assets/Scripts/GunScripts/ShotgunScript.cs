using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : GunScriptParent
{
    protected override void PrivShoot()
    {
        animator.SetBool("OAmmo", Ammo - 1 <= 0);
        base.PrivShoot();

        Color c = new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1)); ;
        for (int i = 0;i < scriptableObject.Burst;i++)
        {
            SpawnBullet(c);
        }
    }

    protected override IEnumerator PrivReload()
    {
        reloading = true;
        if (Ammo > 0)
        {
            Ammo--;
            AmmoGuiChange();
        }
        while (Ammo < scriptableObject.MaxAmmo)
        {
            doneReloading = false;
            animator.SetBool("Reload",true);

            yield return new WaitUntil(() => doneReloading);

            Ammo++;
            AmmoGuiChange();
        }
        animator.SetBool("Reload", false);
        reloading = false;
    } 
}
