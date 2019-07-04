using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To Control the various menus in game
public class GameMenuController : MonoBehaviour
{
    public static GameMenuController instance = null;

    [SerializeField] private PauseMenu pauseMenu;
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

        pauseMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        controlMapperWindow.SetActive(false);
    }

    void Update()
    {
        if (InputController.instance.Pause()) {
            SwapToPauseMenu();
            Time.timeScale = 0f;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            EnemyController.instance.TurnOffFovVisualisation();
        }

        Cursor.visible = true;
    }

    public void SwapToPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void SwapToSettingsMenu()
    {
        settingsMenu.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
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
