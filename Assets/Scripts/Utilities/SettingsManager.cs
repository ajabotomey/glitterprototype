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
    [SerializeField] private float slowdownFactor;

    [Header("Subtitles")]
    [SerializeField] private bool subtitlesEnabled = false;

    [Header("Auto-aim")]
    [SerializeField] private bool autoAimEnabled = false;

    private Resolution[] resolutions;
    private int currentResolutionIndex;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsDyslexicTextEnabled()
    {
        return dyslexicTextEnabled;
    }

    public void DyslexicToggle()
    {
        dyslexicTextEnabled = !dyslexicTextEnabled;
    }
    
    public float CurrentGameSpeed()
    {
        return slowdownFactor;
    }

    public void GameSpeedToggle(int value)
    {
        slowdownFactor = value;
    }

    // Write to PlayerPrefs for the moment, then when the player saves, write to save file
    public void SaveSettings()
    {
        // Apply Changed
        UpdateFont();

        PlayerPrefs.SetFloat("Game Speed", slowdownFactor);
        PlayerPrefs.SetString("Dyslexic Font", "true");
    }

    // Load from PlayerPrefs for the moment until the save system is properly working
    public void LoadSettings()
    {
        slowdownFactor = PlayerPrefs.GetFloat("Game Speed");
        var result = Boolean.Parse(PlayerPrefs.GetString("Dyslexic Font"));
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

    private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        return (currentIndex + 1) % collection.Count;
    }

    private int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        if ((currentIndex - 1) < 0) return collection.Count - 1;
        return (currentIndex - 1) % collection.Count;
    }

    #region Resolution Cycling

    

    #endregion
}
