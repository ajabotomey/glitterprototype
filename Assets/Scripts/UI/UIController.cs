using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject warningFrame;
    [SerializeField] private GameObject diedFrame;
    [SerializeField] private GameObject controlFrame;

    public static UIController instance = null;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Time.timeScale = 0f;
        warningFrame.SetActive(true);
        diedFrame.SetActive(false);
        controlFrame.SetActive(false);
    }

    public void StartGame() {
        warningFrame.SetActive(false);
        EnemyController.instance.TurnOnFovVisualisation();
        Time.timeScale = 1f;
        controlFrame.SetActive(true);
    }

    public void Died() {
        diedFrame.SetActive(true);
        controlFrame.SetActive(false);
        EnemyController.instance.TurnOffFovVisualisation();
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
