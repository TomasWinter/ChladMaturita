using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponInputRelay : MonoBehaviour
{
    BasicGunScript basicGunScript;

    private void Start()
    {
        basicGunScript = GetComponent<BasicGunScript>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            basicGunScript.Shoot(true);
        else if (Input.GetKey(KeyCode.Mouse0))
            basicGunScript.Shoot(false);
        else if (Input.GetKey(KeyCode.R))
            basicGunScript.Reload();
    }
}
