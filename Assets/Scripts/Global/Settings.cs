using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Settings
{
    public static Keybinds Keybinds { get; set; } = new Keybinds();

    public static float Sensitivity { get; set; } = 0.25f;
}
public class Keybinds
{
    public KeyCode Forward { get; set; } = KeyCode.W;
    public KeyCode Backward { get; set;} = KeyCode.S;
    public KeyCode Left { get; set; } = KeyCode.A;
    public KeyCode Right { get; set; } = KeyCode.D;
    public KeyCode Jump { get; set;} = KeyCode.Space;
    public KeyCode Reload { get; set; } = KeyCode.R;
    public KeyCode Interact {  get; set;} = KeyCode.E;
    public KeyCode ThrowBag {  get; set;} = KeyCode.G;
    public KeyCode HideWeapon {  get; set;} = KeyCode.H;
    public KeyCode Weapon1 {  get; set;} = KeyCode.Alpha1;
    public KeyCode Weapon2 { get; set; } = KeyCode.Alpha2;
    public KeyCode Equipment { get; set; } = KeyCode.Alpha4;
}