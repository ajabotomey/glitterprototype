using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject warningFrame;
    [SerializeField] private GameObject diedFrame;

    public static UIController instance = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Time.timeScale = 0f;
        warningFrame.SetActive(true);
        diedFrame.SetActive(false);
    }

    public void StartGame() {
        warningFrame.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Died() {
        diedFrame.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
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
