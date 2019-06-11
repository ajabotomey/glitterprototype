using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    // Settings for player
    public float gameSpeed;
    public bool autoAimEnabled;
    public int autoAimStrength;
    public bool dyslexicTextEnabled;
    public int windowWidth;
    public int windowHeight;
    public bool fullscreenEnabled;
    public int inputSensitivity;
    public bool rumbleEnabled;
    public int rumbleSensitivity;

    public SettingsData()
    {
        gameSpeed = SettingsManager.Instance.CurrentGameSpeed();
        autoAimEnabled = SettingsManager.Instance.IsAutoAimEnabled();
        autoAimStrength = SettingsManager.Instance.GetAutoAimStrength();
        dyslexicTextEnabled = SettingsManager.Instance.IsDyslexicTextEnabled();
        windowWidth = SettingsManager.Instance.GetWindowWidth();
        windowHeight = SettingsManager.Instance.GetWindowHeight();
        fullscreenEnabled = SettingsManager.Instance.IsFullscreenEnabled();
        inputSensitivity = SettingsManager.Instance.GetInputSensitivity();
        rumbleEnabled = SettingsManager.Instance.IsRumbleEnabled();
        rumbleSensitivity = SettingsManager.Instance.GetRumbleSensitivity();
    }
}
