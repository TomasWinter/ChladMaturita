using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalSetterScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletParent;
    private void Awake()
    {
        //GlobalVals.Bullet = bullet;
        //GlobalVals.BulletParent = bulletParent;

        Destroy(this);
    }
}
