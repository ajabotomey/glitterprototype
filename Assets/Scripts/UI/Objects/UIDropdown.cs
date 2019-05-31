using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDropdown : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    private string currentValue;

    // Start is called before the first frame update
    void Awake()
    {
        dropdown.ClearOptions();
    }

    public void SetOptions(List<string> options)
    {
        dropdown.AddOptions(options);
        currentValue = options[0];
    }

    public void SetValue(Dropdown change)
    {
        currentValue = dropdown.options[change.value].text;
    }

    public string GetValue()
    {
        return currentValue;
    }
}
