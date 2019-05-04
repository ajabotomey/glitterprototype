using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleControl : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource source;

    void Start()
    {
        source.clip = sound;
        source.loop = false;
    }

    void Update()
    {
        bool whistle = Input.GetButtonDown("Whistle");

        if (whistle && source.isPlaying)
        {
            Debug.Log("Should be whistling.");
            Whistle();
        }
    }

    public void Whistle()
    {
        source.Play();
    }
}
