using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [Header("Fonts")]
    [SerializeField] private Font classicFont;
    [SerializeField] private Font dyslexicFont;
    [SerializeField] private bool dyslexicTextEnabled;

    [Header("Game Speed")]
    [SerializeField] private float slowdownFactor = 100;

    [Header("Subtitles")]
    [SerializeField] private bool subtitlesEnabled = false;

    [Header("Auto-aim")]
    [SerializeField] private bool autoAimEnabled = false;
    [SerializeField][Range(0, 100)] private int autoAimStrength = 100;

    [Header("Fullscreen")]
    [SerializeField] private bool fullscreenEnabled = false;

    private Resolution[] resolutions;
    private int currentResolutionIndex;
    private List<string> resolutionsSupported;

    private int width, height;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
            return;
        }

        resolutions = Screen.resolutions;
        resolutionsSupported = new List<string>();

        for (int i = 3; i < resolutions.Length; i++) {

            var s = resolutions[i].ToString();
            var delimiter = s.IndexOf(" @");

            resolutionsSupported.Add(s.Substring(0, delimiter));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Accessor Methods

    public bool IsDyslexicTextEnabled()
    {
        return dyslexicTextEnabled;
    }

    public bool IsFullscreenEnabled()
    {
        return fullscreenEnabled;
    }

    public float CurrentGameSpeed()
    {
        return slowdownFactor;
    }

    public List<string> ResolutionsSupported()
    {
        return resolutionsSupported;
    }

    public bool IsAutoAimEnabled()
    {
        return autoAimEnabled;
    }

    public int GetAutoAimStrength()
    {
        return autoAimStrength;
    }

    #endregion

    #region Mutator Methods

    public void GameSpeedToggle(int value)
    {
        slowdownFactor = value;
    }

    public void DyslexicToggle()
    {
        dyslexicTextEnabled = !dyslexicTextEnabled;
    }

    public void SetResolution(string value)
    {
        var resolutionString = value;
        string[] values = resolutionString.Split(new string[] { " x " }, StringSplitOptions.RemoveEmptyEntries);

        width = Int32.Parse(values[0]);
        height = Int32.Parse(values[1]);

        Screen.SetResolution(width, height, fullscreenEnabled);
    }

    public void FullScreenToggle()
    {
        fullscreenEnabled = !fullscreenEnabled;
    }

    public void AutoAimToggle()
    {
        autoAimEnabled = !autoAimEnabled;
    }

    public void SetAutoAimStrength(int value)
    {
        autoAimStrength = value;
    }

    #endregion



    // Write to PlayerPrefs for the moment, then when the player saves, write to save file
    public void SaveSettings()
    {
        // Apply Changed
        UpdateFont();

        PlayerPrefs.SetFloat("Game Speed", slowdownFactor);
        PlayerPrefs.SetString("Dyslexic Font", dyslexicTextEnabled.ToString());
        PlayerPrefs.SetString("Fullscreen", fullscreenEnabled.ToString());
        PlayerPrefs.SetInt("Width", width);
        PlayerPrefs.SetInt("Height", height);
        PlayerPrefs.SetString("Auto-aim", autoAimEnabled.ToString());
        PlayerPrefs.SetInt("Auto-aim Strength", autoAimStrength);
    }

    // Load from PlayerPrefs for the moment until the save system is properly working
    public void LoadSettings()
    {
        slowdownFactor = PlayerPrefs.GetFloat("Game Speed");
        dyslexicTextEnabled = Boolean.Parse(PlayerPrefs.GetString("Dyslexic Font"));
        fullscreenEnabled = Boolean.Parse(PlayerPrefs.GetString("Fullscreen"));
        width = PlayerPrefs.GetInt("Width");
        height = PlayerPrefs.GetInt("Height");
        autoAimEnabled = Boolean.Parse(PlayerPrefs.GetString("Auto-aim"));
        autoAimStrength = PlayerPrefs.GetInt("Auto-aim Strength");
    }

    public void UpdateFont()
    {
        var textComponents = Component.FindObjectsOfType<Text>();

        if (dyslexicTextEnabled) { // Change all text to use Dyslexic font
            foreach (var component in textComponents)
                component.font = dyslexicFont;
        } else { // Change back to Arial
            foreach (var component in textComponents) {
                if (component.gameObject.name != "DyslexicText")
                    component.font = classicFont;
            }
        }
    }
}
