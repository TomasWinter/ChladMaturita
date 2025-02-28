using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon",menuName = "ScriptableObjects/Weapon")]
public class WeaponSO : ScriptableObject
{
    private static int LastID = 0;

    public int ID = -1;
    public float BulletForce = 25;
    public int Damage = 0;
    public float FireRate = 0;
    public bool FullAuto = true;
    public int MaxAmmo = 0;
    public float Spread = 0;
    public int Burst = 1;
    public GameObject Prefab;
    public RuntimeAnimatorController AnimatorController;
    public Sprite GunImage;

    private void OnValidate()
    {
        if (ID == -1)
        {
            ID = LastID++;
        }
    }
}
