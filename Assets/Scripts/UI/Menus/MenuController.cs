﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controlling the various menus within the main menu
public class MenuController : MonoBehaviour
{
    public static MenuController instance = null;

    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private GameObject controlMapperWindow;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
        controlMapperWindow.SetActive(false);
    }

    public void SwapToMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void SwapToSettingsMenu()
    {
        settingsMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        controlMapperWindow.SetActive(false);
    }

    public void SwapToControlMapper()
    {
        settingsMenu.gameObject.SetActive(false);
        controlMapperWindow.SetActive(true);
    }

    public void ReturnFromControlMapper()
    {
        SwapToSettingsMenu();
        settingsMenu.GetComponent<SettingsMenu>().ReturnFromControlMapper();
    }
}
