using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponInputRelay : MonoBehaviour
{
    GunScriptParent gunScript;

    private void Start()
    {
        gunScript = GetComponent<GunScriptParent>();
    }
    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                gunScript.Shoot(true);
            else if (Input.GetKey(KeyCode.Mouse0))
                gunScript.Shoot(false);
            else if (Input.GetKey(Settings.Keybinds.Reload))
                gunScript.Reload();
        }
    }
}
