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
    [SerializeField] private GameObject weaponWheel;

    public static UIController instance = null;

    public bool isInMenu = true;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        warningFrame.SetActive(true);
        diedFrame.SetActive(false);
        controlFrame.SetActive(false);
        weaponWheel.SetActive(false);
        Cursor.visible = true;
        isInMenu = true;
    }

    void Update()
    {
        SettingsManager.Instance.UpdateFont();
    }


    // TODO: TO BE REMOVED
    public void StartGame() {
        warningFrame.SetActive(false);
        EnemyController.instance.TurnOnFovVisualisation();
        Time.timeScale = 1f;
        controlFrame.SetActive(true);
        Cursor.visible = false;
        isInMenu = false;
    }

    // TODO: TO BE REMOVED
    public void Died() {
        diedFrame.SetActive(true);
        controlFrame.SetActive(false);
        EnemyController.instance.TurnOffFovVisualisation();
        Time.timeScale = 0f;
        Cursor.visible = true;
        isInMenu = true;
    }


    // TODO: TO BE REMOVED
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // TODO: TO BE REMOVED
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

    public void ShowWeaponWheel()
    {
        Cursor.visible = true;
        weaponWheel.SetActive(true);
    }

    public void HideWeaponWheel()
    {
        if (WeaponControl.instance.CurrentWeapon != WeaponControl.WeaponState.NOISE)
            Cursor.visible = false;

        weaponWheel.SetActive(false);
    }

    public void SelectGun()
    {
        weaponWheel.SetActive(false);
        WeaponControl.instance.SelectGun();
    }

    public void SelectNoise()
    {
        weaponWheel.SetActive(false);
        WeaponControl.instance.SelectNoise();
    }

    public void SelectMask()
    {
        weaponWheel.SetActive(false);
        WeaponControl.instance.SelectMask();
    }
}
