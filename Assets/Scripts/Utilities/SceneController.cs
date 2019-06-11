using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;

    [SerializeField] private string mainMenu;
    [SerializeField] private string[] gameScenes;
    private int currentSceneIndex; // 0 for main menu, 1 for test level

    void Awake()
    {
        instance = this;
    }

    public void LoadGame()
    {
        //SceneManager.LoadScene(gameScenes[0]);
        SceneManager.LoadScene(gameScenes[0]);
        currentSceneIndex = 1;
    }

    /*
     * Method for loading game with save file here
     * 
     */

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        currentSceneIndex = 0;
    }

    public bool IsInGame()
    {
        return currentSceneIndex != 0;
    }

    // Add loading screen here
    IEnumerator LoadLevel(string levelName)
    {
        enabled = false;
        yield return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));
        enabled = true;
    }
}
