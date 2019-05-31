using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private UISlider gameSpeedSlider;
    [SerializeField] private UIToggle dyslexicTextToggle;
    [SerializeField] private UIDropdown resolutionDropdown;
    [SerializeField] private UIToggle fullscreenToggle;
    [SerializeField] private UIToggle autoAimToggle;
    [SerializeField] private UISlider autoAimStrengthSlider;

    // Start is called before the first frame update
    void Start()
    {
        var gameSpeed = SettingsManager.Instance.CurrentGameSpeed();
        var dyslexicText = SettingsManager.Instance.IsDyslexicTextEnabled();
        var supportedResolutions = SettingsManager.Instance.ResolutionsSupported();
        var fullscreenEnabled = SettingsManager.Instance.IsFullscreenEnabled();
        var autoAimEnabled = SettingsManager.Instance.IsAutoAimEnabled();
        var autoAimStrength = SettingsManager.Instance.GetAutoAimStrength();

        gameSpeedSlider.SetValue((int)gameSpeed);
        dyslexicTextToggle.SetValue(dyslexicText);
        resolutionDropdown.SetOptions(supportedResolutions);
        fullscreenToggle.SetValue(fullscreenEnabled);
        autoAimToggle.SetValue(autoAimEnabled);
        autoAimStrengthSlider.SetValue(autoAimStrength);
    }

    // Update is called once per frame
    void Update()
    {
        SettingsManager.Instance.UpdateFont();

        if (SettingsManager.Instance.IsAutoAimEnabled()) {
            autoAimStrengthSlider.gameObject.SetActive(true);
        } else {
            autoAimStrengthSlider.gameObject.SetActive(false);
        }
    }

    public void ApplyChanges()
    {
        SettingsManager.Instance.SetResolution(resolutionDropdown.GetValue());
        SettingsManager.Instance.SaveSettings();
    }
}
