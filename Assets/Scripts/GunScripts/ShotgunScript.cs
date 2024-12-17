using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : GunScriptParent
{
    protected override void PrivShoot()
    {
        Ammo--;
        animator.SetBool("OAmmo", Ammo <= 0);
        AmmoGuiChange();
        animator.SetTrigger("Shoot");
        AudioManager.Play(gameObject, shotAudio, 30f, 1f, AudioManager.RandomPitch(0.1f)-0.5f);

        Color? c = null;
        for (int i = 0;i < scriptableObject.Burst;i++)
        {
            c = ShotgunShoot(c);
        }
    }

    private Color ShotgunShoot(Color? c = null)
    {
        GameObject bullet = Instantiate(GlobalVals.Instance.Bullet, bulletSpawn.position, bulletSpawn.rotation * Quaternion.Euler(0, -90, 0), GlobalVals.Instance.BulletParent.transform);
        BulletScript bs = bullet.GetComponent<BulletScript>();
        bs.Constructor(scriptableObject.Damage,owner,null,c);

        Rigidbody brb = bullet.GetComponent<Rigidbody>();
        Vector3 rndVector = new Vector3(Random.Range(-scriptableObject.Spread, scriptableObject.Spread), Random.Range(-scriptableObject.Spread, scriptableObject.Spread),1);
        brb.AddRelativeForce(rndVector * scriptableObject.BulletForce, ForceMode.VelocityChange);

        return (Color)bs.color;
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
