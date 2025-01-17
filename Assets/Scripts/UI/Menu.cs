using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] bool Default;
    public Menu Previous;

    private void Start()
    {
        if (Default)
            MenuBrowser.Instance.current = this;
    }
}
