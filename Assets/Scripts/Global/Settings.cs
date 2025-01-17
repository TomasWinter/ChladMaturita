using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Settings
{
    public static Settings Instance { get; private set; }
    public Keybinds Keybinds { get; set; } = new Keybinds();

    public float Sensitivity { get; set; } = 1.0f;

    public Settings()
    {
        if (Instance == null)
            Instance = this;
    }
}
[System.Serializable]
public class Keybinds
{
    public KeyCode Forward { get; set; } = KeyCode.W;
    public KeyCode Backward { get; set;} = KeyCode.S;
    public KeyCode Left { get; set; } = KeyCode.A;
    public KeyCode Right { get; set; } = KeyCode.D;
    public KeyCode Jump { get; set;} = KeyCode.Space;
    public KeyCode Interact {  get; set;} = KeyCode.E;
    public KeyCode ThrowBag {  get; set;} = KeyCode.G;
}