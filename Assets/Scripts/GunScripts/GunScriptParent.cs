using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunScriptParent : MonoBehaviour,IAnimationReactor
{
    protected WeaponSO scriptableObject;
    public int Ammo { get; protected set; }
    protected Animator animator;

    [SerializeField] protected Transform bulletSpawn;
    [Header("Audio")]
    [SerializeField] protected AudioClip shotAudio;

    protected float timer = 0;
    protected bool reloading = true;
    protected bool doneReloading = false;

    protected bool uiChanger = false;
    protected OwnerType owner = OwnerType.Enemy;
    protected void Start()
    {
        AmmoGuiChange();
        if (Ammo == -1)
            Ammo = scriptableObject.MaxAmmo;
        
    }
    protected void Update()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, scriptableObject.FireRate);
    }

    public void Shoot(bool fireAlways = true)
    {
        if (!reloading && timer <= 0 && (fireAlways || scriptableObject.FullAuto))
        {
            if (Ammo > 0)
            {
                PrivShoot();
            }
            else
            {
                Reload();
            }
            timer = scriptableObject.FireRate;
        }
    }

    protected virtual void PrivShoot()
    {
        Ammo--;
        AmmoGuiChange();
        animator.SetTrigger("Shoot");
        AudioManager.Play(gameObject, shotAudio, 20f, 1f, AudioManager.RandomPitch(0.1f));
    }

    public void Reload()
    {
        if (timer <= 0 && !reloading)
        {
            reloading = true;
            StartCoroutine(PrivReload());
        }
    }

    protected virtual IEnumerator PrivReload()
    {
        reloading = true;
        doneReloading = false;
        animator.SetTrigger("Reload");

        yield return new WaitUntil(() => doneReloading);

        animator.ResetTrigger("Reload");
        Ammo = scriptableObject.MaxAmmo;
        AmmoGuiChange();
        reloading = false;
    }

    public virtual void AnimDone(string name = "")
    {
        if (!doneReloading && name == "Reload")
            doneReloading = true;
    }

    public virtual void OnWeaponAwake(WeaponSO _scriptableObject, int _ammo, Animator _animator, bool isUiChanger = false)
    {
        uiChanger = isUiChanger;
        scriptableObject = _scriptableObject;
        animator = _animator;
        if (_ammo > -1)
            Ammo = _ammo;
        else
            Ammo = scriptableObject.MaxAmmo;

        if (uiChanger)
            owner = OwnerType.Player;

        AmmoGuiChange();

        reloading = false;
    }

    public virtual void OnWeaponDestroy()
    {
        if (uiChanger)
            PlayerGuiManager.Instance.ChangeAmmo(0, 0);
    }

    protected void AmmoGuiChange()
    {
        if (uiChanger)
            PlayerGuiManager.Instance.ChangeAmmo(Ammo, scriptableObject.MaxAmmo);
    }
}
