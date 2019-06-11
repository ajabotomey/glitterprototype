using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;

    [SerializeField] private string mainMenu;
    [SerializeField] private string[] gameScenes;
    private int currentSceneIndex; // 0 for main menu, 1 for test level

    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

            canvas.worldCamera = Camera.main;
        } else {
            DestroyImmediate(gameObject);
            return;
        }
    }

    public void LoadGame()
    {
        //SceneManager.LoadScene(gameScenes[0]);
        StartCoroutine(LoadLevel(gameScenes[0]));
        currentSceneIndex = 1;
    }

    /*
     * Method for loading game with save file here
     * 
     */

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(mainMenu));
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
        //yield return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        loadingScreen.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }

        enabled = true;

        loadingScreen.SetActive(false);

        // Reassign the camera
        canvas.worldCamera = Camera.main;
    }
}
