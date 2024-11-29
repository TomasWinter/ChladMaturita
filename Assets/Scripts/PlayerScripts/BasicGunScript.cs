using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicGunScript : MonoBehaviour,IAnimationReactor
{
    WeaponSO scriptableObject;
    public int Ammo { get; private set; }
    Animator animator;

    [SerializeField] Transform bulletSpawn;

    float timer = 0;
    bool reloading = true;
    bool dunnit = false;

    bool uiChanger = false;
    private void Start()
    {
        AmmoGuiChange();
        if (Ammo == -1)
            Ammo = scriptableObject.MaxAmmo;
    }
    private void Update()
    {
        timer = Mathf.Clamp(timer-Time.deltaTime,0,scriptableObject.FireRate);
    }

    public void Shoot(bool fireAlways = true)
    {
        if (!reloading && timer <= 0 && (fireAlways || scriptableObject.FullAuto))
        {
            timer = scriptableObject.FireRate;
            if (Ammo > 0)
            {
                Ammo--;
                AmmoGuiChange();
                animator.SetTrigger("Shoot");

                GameObject bullet = Instantiate(GlobalVals.Instance.Bullet, bulletSpawn.position, bulletSpawn.rotation * Quaternion.Euler(0,-90,0), GlobalVals.Instance.BulletParent.transform);
                bullet.GetComponent<BulletScript>().Constructor(scriptableObject.Damage);

                Rigidbody brb = bullet.GetComponent<Rigidbody>();
                brb.AddRelativeForce(Vector3.forward * scriptableObject.BulletForce, ForceMode.VelocityChange);
            }
            else
                StartCoroutine(PrivReload());
        }
    }

    public void Reload()
    {
        if (timer <= 0)
            StartCoroutine(PrivReload());
    }

    IEnumerator PrivReload()
    {
        reloading = true;
        dunnit = false;
        animator.SetTrigger("Reload");

        yield return new WaitUntil(() => dunnit);

        animator.ResetTrigger("Reload");
        Ammo = scriptableObject.MaxAmmo;
        AmmoGuiChange();
        reloading = false;
    }

    public void AnimDone(string name = "")
    {
        if (!dunnit && name == "Reload")
            dunnit = true;
    }

    private void AmmoGuiChange()
    {
        if (uiChanger)
            PlayerGuiManager.Instance.ChangeAmmo(Ammo, scriptableObject.MaxAmmo);
    }

    public void OnWeaponAwake(WeaponSO _scriptableObject,int _ammo,Animator _animator,bool isUiChanger = false)
    {
        uiChanger = isUiChanger;
        scriptableObject = _scriptableObject;
        animator = _animator;
        if (_ammo > -1)
            Ammo = _ammo;
        else
            Ammo = scriptableObject.MaxAmmo;

        AmmoGuiChange();

        reloading = false;
    }

    public void OnWeaponDestroy()
    {
        if (uiChanger)
            PlayerGuiManager.Instance.ChangeAmmo(0, 0);
    }
}
