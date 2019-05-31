using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance = null;

    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private SettingsMenu settingsMenu;

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
    }
}
