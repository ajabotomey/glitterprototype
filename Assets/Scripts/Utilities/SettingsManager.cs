using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [Header("Fonts")]
    [SerializeField] private Font classicFont;
    [SerializeField] private Font dyslexicFont;

    [Header("Game Speed")]
    [SerializeField] private float slowdownFactor;

    [Header("Subtitles")]
    [SerializeField] private bool subtitlesEnabled = false;

    [Header("Auto-aim")]
    [SerializeField] private bool autoAimEnabled = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
