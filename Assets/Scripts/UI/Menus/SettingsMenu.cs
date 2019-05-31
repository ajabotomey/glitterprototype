using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private UISlider gameSpeedSlider;
    [SerializeField] private UIToggle dyslexicTextToggle;
    private Dropdown dropdown;

    private float gameSpeed;
    private bool dyslexicText;

    // Start is called before the first frame update
    void Start()
    {
        gameSpeed = SettingsManager.Instance.CurrentGameSpeed();
        dyslexicText = SettingsManager.Instance.IsDyslexicTextEnabled();

        gameSpeedSlider.SetValue((int)gameSpeed);
        dyslexicTextToggle.SetValue(dyslexicText);

    }

    // Update is called once per frame
    void Update()
    {
        SettingsManager.Instance.UpdateFont();
    }

    public void ApplyChanges()
    {
        SettingsManager.Instance.SaveSettings();
    }
}
