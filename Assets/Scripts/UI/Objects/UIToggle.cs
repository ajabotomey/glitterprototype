using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    
    public void SetValue(bool value)
    {
        toggle.isOn = value;
    }

    public Toggle GetObject()
    {
        return toggle;
    }
}
