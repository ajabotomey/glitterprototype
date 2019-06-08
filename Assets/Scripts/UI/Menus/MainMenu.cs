using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitGameButton;

    void OnEnable()
    {
        SettingsManager.Instance.UpdateFont();
        startGameButton.Select();
        startGameButton.OnSelect(null);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadGame()
    {

    }

    public void SettingsMenu()
    {
        MenuController.instance.SwapToSettingsMenu();
    }

    public void QuitGame()
    {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
        Application.Quit();
#elif (UNITY_WEBGL)
        Application.OpenURL("about:blank");
#endif
    }
}
