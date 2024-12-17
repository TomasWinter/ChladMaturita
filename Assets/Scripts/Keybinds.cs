using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Keybinds
{
    public static KeyCode Forward { get; private set; } = KeyCode.W;
    public static KeyCode Backward { get; private set; } = KeyCode.S;
    public static KeyCode Left { get; private set; } = KeyCode.A;
    public static KeyCode Right { get; private set; } = KeyCode.D;

    public static KeyCode Jump { get; private set; } = KeyCode.Space;

    public static KeyCode Interact { get; private set; } = KeyCode.E;
    public static KeyCode ThrowBag { get; private set; } = KeyCode.G;
}
