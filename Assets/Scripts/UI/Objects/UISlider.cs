using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text value;
    [SerializeField] private string fieldName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var sliderValue = Mathf.RoundToInt((slider.value / slider.maxValue) * 100);
        value.text = fieldName + ": " + sliderValue;
    }
}
