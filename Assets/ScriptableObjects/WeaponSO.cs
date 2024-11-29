using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon",menuName = "ScriptableObjects/Weapon")]
public class WeaponSO : ScriptableObject
{
    public int ID = 0;
    public float BulletForce = 25;
    public int Damage = 0;
    public float FireRate = 0;
    public bool FullAuto = true;
    public int MaxAmmo = 0;
    public GameObject Prefab;
    public AnimatorOverrideController AnimatorOverrideController;
}
