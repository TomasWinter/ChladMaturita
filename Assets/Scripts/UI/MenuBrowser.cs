using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuBrowser : MonoBehaviour
{
    public static MenuBrowser Instance { get; private set; }

    [SerializeField] List<Menu> menus = new();

    public Menu current;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && current?.Previous != null)
        {
            SwitchTo(current.Previous);
        }
    }

    public void SwitchTo(Menu menu)
    {
        current.gameObject.GetComponent<Canvas>().sortingOrder = -1;
        current = menu;
        current.gameObject.GetComponent<Canvas>().sortingOrder = 10;
    }

    public void GoBack()
    {
        current.gameObject.GetComponent<Canvas>().sortingOrder = -1;
        current = current.Previous;
        current.gameObject.GetComponent<Canvas>().sortingOrder = 10;
    }
}